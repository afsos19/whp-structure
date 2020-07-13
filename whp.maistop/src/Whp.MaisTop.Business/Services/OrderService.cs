using AutoMapper;
using NLog;
using Ssg.MaisSamsung.Business.Dto.Ltm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class OrderService : IOrderService
    {

        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderStatusRepository _orderStatusRepository;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPunctuationRepository _userPunctuationRepository;
        private readonly IUserPunctuationSourceRepository _userPunctuationSourceRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IOrderReversalItemRepository _orderReversalItemRepository;

        public OrderService(
            IOrderReversalItemRepository orderReversalItemRepository,
            IUserPunctuationRepository userPunctuationRepository,
            IUserPunctuationSourceRepository userPunctuationSourceRepository,
            IUserRepository userRepository,
            ILogger logger,
            IOrderRepository orderRepository,
            IOrderItemRepository orderItemRepository,
            IOrderStatusRepository orderStatusRepository,
            IUnitOfWork unitOfWork,
            IShopUserRepository shopUserRepository,
            IMapper mapper)
        {
            _orderReversalItemRepository = orderReversalItemRepository;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _shopUserRepository = shopUserRepository;
            _userPunctuationRepository = userPunctuationRepository;
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _orderStatusRepository = orderStatusRepository;
            _mapper = mapper;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
        }

        public async Task<IEnumerable<OrderReturnDto>> GetUserOrder(int userId, int currentMonth, int currentYear)
        {

            var orderItems = await _orderItemRepository.CustomFind(x => x.Order.User.Id == userId &&
             x.CreatedAt.Month == currentMonth &&
             x.CreatedAt.Year == currentYear,
             x => x.Order,
             x => x.Order.OrderStatus);

            var orders = new List<OrderReturnDto>();

            orders = _mapper.Map(orderItems.GroupBy(x => x.Order).Select(x => x.Key).ToList(), orders);

            foreach (var item in orders)
            {
                item.Description = string.Join(",", orderItems.Where(x => x.Order.Id == item.Id).Select(x => x.ProductName).ToList());
            }

            return orders;
        }
        public async Task<RescueResponseDto<Order>> DoRescue(RescueRequestDto rescueRequestDto)
        {

            var user = await _userRepository.CustomFind(x => x.Cpf.Equals(rescueRequestDto.Cpf) && (x.UserStatus.Id == (int)UserStatusEnum.Active || x.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog));

            _logger.Info($"resgate para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - iniciada");

            if (!user.Any())
            {
                _logger.Info($"resgate para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - usuario invalido");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "usuario invalido"
                };

            }

            var lastRescue = await _orderRepository.GetLast(x => x.User.Cpf.Equals(rescueRequestDto.Cpf), x => x.Id);

            if (lastRescue != null && (DateTime.Now - lastRescue.CreatedAt).TotalMinutes < 2)
            {
                _logger.Fatal($"resgate para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - suspeita de fraude por tentativa de gerar pedidos em um pequeno intervalo de tempo");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "suspeita de fraude por tentativa de gerar pedidos em um pequeno intervalo de tempo"
                };
            }


            if (!rescueRequestDto.RescueRequestItems.Any())
            {
                _logger.Info($"resgate para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - pedido não possue itens");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "pedido não possue itens"
                };
            }

            var balance = (await _userPunctuationRepository.CustomFind(x => x.User.Id == user.First().Id)).Sum(x => x.Punctuation);
            var order = new Order();

            if (balance < rescueRequestDto.Total)
            {
                _logger.Info($"autorizacao de pedido para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - autorizacao recusada saldo insuficiente");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "Saldo insuficiente"
                };
            }

            order = new Order
            {
                Activated = true,
                ConversionRate = rescueRequestDto.ConversionRate,
                CreatedAt = DateTime.Now,
                ExternalOrderId = rescueRequestDto.ExternalOrderId,
                Freight = rescueRequestDto.Freight,
                Login = rescueRequestDto.Cpf,
                ForecastDate = rescueRequestDto.ForecastDate,
                OrderStatus = await _orderStatusRepository.GetById((int)OrderStatusEnum.Confirmed),
                OrderValue = rescueRequestDto.OrderValue,
                Total = rescueRequestDto.Total,
                User = user.First()
            };

            _orderRepository.Save(order);

            await _unitOfWork.CommitAsync();

            _userPunctuationRepository.Save(new UserPunctuation
            {
                CreatedAt = DateTime.Now,
                CurrentMonth = DateTime.Now.Month,
                CurrentYear = DateTime.Now.Year,
                Description = "Pedido Confirmado",
                OperationType = 'D',
                Punctuation = -1 * rescueRequestDto.Total,
                ReferenceEntityId = order.Id,
                UserPunctuationSource = await _userPunctuationSourceRepository.GetById((int)SourceUserPunctuationEnum.OrderConfirmed),
                User = user.First()
            });

            foreach (var item in rescueRequestDto.RescueRequestItems)
            {

                _orderItemRepository.Save(new OrderItem
                {
                    Activated = true,
                    Ammout = item.Ammout,
                    Category = item.Category,
                    CodeItem = item.CodeItem,
                    CreatedAt = DateTime.Now,
                    Department = item.Department,
                    ExternalOrderId = rescueRequestDto.ExternalOrderId,
                    Order = order,
                    Partner = item.Partner,
                    ProductName = item.ProductName,
                    TotalValue = item.TotalValue,
                    UnitValue = item.UnitValue
                });

            }

            var confirmSaved = await _unitOfWork.CommitAsync();

            if (confirmSaved)
            {

                order.User = null;

                _logger.Info($"autorizacao de pedido para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - Pedido gerado com sucesso!");
                return new RescueResponseDto<Order>
                {
                    Success = true,
                    Message = "Pedido gerado com sucesso!",
                    Data = new List<Order> { order }
                };
            }
            else
            {
                _logger.Info($"autorizacao de pedido para login {rescueRequestDto.Cpf} com codigo externo {rescueRequestDto.ExternalOrderId} - autorizacao recusada ocorreu erro ao tentar gerar o pedido");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "ocorreu erro ao tentar gerar o pedido"
                };
            }

        }

        public async Task<RescueResponseDto<Order>> DoReversal(ReversalRequestDto reversalRequestDto)
        {

            var user = await _userRepository.CustomFind(x => x.Cpf.Equals(reversalRequestDto.Cpf) && (x.UserStatus.Id == (int)UserStatusEnum.Active || x.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog));

            _logger.Info($"estorno de pedido para login {reversalRequestDto.Cpf} - iniciada");

            if (!user.Any())
            {
                _logger.Info($"estorno de pedido para login {reversalRequestDto.Cpf} com codigo externo {reversalRequestDto.ExternalOrderId} - usuario invalido");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "usuario invalido"
                };
            }

            var order = await _orderRepository.CustomFind(x =>
            x.OrderStatus.Id == (int)OrderStatusEnum.Confirmed &&
            x.ExternalOrderId == reversalRequestDto.ExternalOrderId &&
            x.Login.Equals(reversalRequestDto.Cpf)
            && x.User.Id == user.First().Id, x => x.User, x => x.OrderStatus);

            if (!order.Any())
            {
                _logger.Fatal($"estorno de pedido para login {reversalRequestDto.Cpf} com codigo externo {reversalRequestDto.ExternalOrderId}  - estorno recusado nenhum pedido encontrado nesse status");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "Pedido não encontrado",
                };
            }

            var orderItems = await _orderItemRepository.CustomFind(x => x.ExternalOrderId == reversalRequestDto.ExternalOrderId && x.Order.Id == order.First().Id);

            if (!orderItems.Any() || !orderItems.Where(x => reversalRequestDto.ReversalRequestItems.Select(y => y.CodeItem).ToList().Contains(x.CodeItem)).Any())
            {
                _logger.Fatal($"estorno de pedido para login {reversalRequestDto.Cpf} com codigo externo {reversalRequestDto.ExternalOrderId}  - estorno recusado itens não encontrado");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "itens não encontrado",
                };
            }

            var itemsReversed = await _orderReversalItemRepository.CustomFind(x => x.OrderItem.ExternalOrderId == reversalRequestDto.ExternalOrderId && x.OrderItem.Order.Id == order.First().Id);

            if(itemsReversed.Where(x => reversalRequestDto.ReversalRequestItems.Select(y => y.CodeItem).ToList().Contains(x.CodeItem)).Any())
            {
                _logger.Fatal($"estorno de pedido para login {reversalRequestDto.Cpf} com codigo externo {reversalRequestDto.ExternalOrderId}  - estorno recusado item já estornado");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "estorno recusado item já estornado",
                };
            }

            if(reversalRequestDto.ReversalRequestItems.Sum(x => x.TotalValue) == order.First().Total ||
                (itemsReversed.Any() && (itemsReversed.Sum(x => x.TotalValue) + reversalRequestDto.ReversalRequestItems.Sum(x => x.TotalValue)) == order.First().Total))
            {
                order.First().OrderStatus = await _orderStatusRepository.GetById((int)OrderStatusEnum.Reversed);
                order.First().ReversedAt = DateTime.Now;
            }
            
            foreach (var item in reversalRequestDto.ReversalRequestItems)
            {
                var orderReversalItem = new OrderReversalItem
                {
                    Ammout = item.Ammout,
                    CodeItem = item.CodeItem,
                    CreatedAt = DateTime.Now,
                    ExternalOrderId = item.ExternalOrderId,
                    OrderItem = orderItems.Where(x => x.CodeItem == item.CodeItem).First(),
                    Reason = item.Reason,
                    TotalValue = item.TotalValue,
                    UnitValue = item.UnitValue
                };

                _orderReversalItemRepository.Save(orderReversalItem);

                await _unitOfWork.CommitAsync();

                _userPunctuationRepository.Save(new UserPunctuation
                {
                    CreatedAt = DateTime.Now,
                    Punctuation = item.TotalValue,
                    ReferenceEntityId = orderReversalItem.Id,
                    UserPunctuationSource = await _userPunctuationSourceRepository.GetById(3),
                    User = user.First(),
                    Description = $"Estorno pedido {orderReversalItem.Id} ",
                    CurrentMonth = DateTime.Now.Month,
                    CurrentYear = DateTime.Now.Year,
                    OperationType = 'C'
                });
            }

            var reversalSaved = await _unitOfWork.CommitAsync();

            if (reversalSaved)
            {
                order.First().User = null;
                _logger.Info($"estorno de pedido para login {reversalRequestDto.Cpf} com codigo externo {reversalRequestDto.ExternalOrderId} - concluido com sucesso");
                return new RescueResponseDto<Order>
                {
                    Success = true,
                    Data = order,
                    Message = "OK"
                };
            }
            else
            {
                _logger.Info($"estorno de pedido para login {reversalRequestDto.Cpf} com codigo externo {reversalRequestDto.ExternalOrderId} - estorno recusado ocorreu um erro ao tenta gerar estorno");
                return new RescueResponseDto<Order>
                {
                    Success = false,
                    Message = "Ocorreu um erro ao tentar gerar o estorno",
                };
            }

        }

        public async Task<RescueResponseDto<decimal>> UserBalance(string cpf)
        {
            var user = await _userRepository.CustomFind(x => x.Cpf.Equals(cpf) && (x.UserStatus.Id == (int)UserStatusEnum.Active || x.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog));

            if (!user.Any())
                return new RescueResponseDto<decimal>
                {
                    Message = "Usuario não encontrado",
                    Success = false,
                };

            var balance = (await _userPunctuationRepository.CustomFind(x => x.User.Id == user.First().Id)).Sum(x => x.Punctuation);

            _logger.Info($"consulta de saldo para login {cpf} saldo {balance}");

            return new RescueResponseDto<decimal>
            {
                Data = new List<decimal> { balance > 0 ? balance : 0 },
                Message = "Saldo encontrado com sucesso!",
                Success = true,
            };
        }
    }
}
