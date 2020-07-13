using AutoMapper;
using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Mapping;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;
using Xunit;

namespace Whp.MaisTop.Tests.Whp.MaisTop.Business.Services
{
    public class OrderServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IMapper _mapper;
        private Mock<IOrderReversalItemRepository> _mockOrderReversalItemRepository;
        private Mock<IShopUserRepository> _mockShopUserRepository;
        private Mock<IUserPunctuationSourceRepository> _mockUserPunctuationSourceRepository;
        private Mock<IUserPunctuationRepository> _mockUserPunctuationRepository;
        private Mock<ILogger> _mockLogger;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IOrderStatusRepository> _mockOrderStatusRepository;
        private Mock<IOrderItemRepository> _mockOrderItemRepository;
        private Mock<IOrderRepository> _mockOrderRepository;

        public OrderServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockOrderReversalItemRepository = new Mock<IOrderReversalItemRepository>();
            _mockUserPunctuationSourceRepository = new Mock<IUserPunctuationSourceRepository>();
            _mockUserPunctuationRepository = new Mock<IUserPunctuationRepository>();
            _mockLogger = new Mock<ILogger>();
            _mockShopUserRepository = new Mock<IShopUserRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockOrderStatusRepository = new Mock<IOrderStatusRepository>();
            _mockOrderItemRepository = new Mock<IOrderItemRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();

            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);

        }

        [Fact]
        public async Task GetUserOrder_GettingOrderResult_ReturningOrderWithDescription()
        {
            _mockOrderItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderItem, bool>>>(), It.IsAny<Expression<Func<OrderItem, object>>>(), It.IsAny<Expression<Func<OrderItem, object>>>())).ReturnsAsync(new List<OrderItem> { new OrderItem { Order = new Order { Id = 10 }, ProductName = "Nome do Produto1" }, new OrderItem { Order = new Order { Id = 11 }, ProductName = "Nome do Produto2" } });

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var result = await service.GetUserOrder(123, 10, 2019);

            Assert.IsType<List<OrderReturnDto>>(result);
            Assert.Equal("Nome do Produto1", result.First().Description);
            Assert.Equal("Nome do Produto2", result.Last().Description);

        }

        [Fact]
        public async Task UserBalance_Balance_ReturingBalance()
        {
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10 } });
            _mockUserPunctuationRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<UserPunctuation, bool>>>())).ReturnsAsync(new List<UserPunctuation> { new UserPunctuation { Id = 10, Punctuation = 100 } });

            var user = "11111111111";

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var result = await service.UserBalance(user);


            Assert.True(result.Success);
            Assert.IsType<List<decimal>>(result.Data);
            Assert.Single(result.Data);
            Assert.Equal(new List<decimal> { 100 }, result.Data);

        }

        [Fact]
        public async Task DoReversal_ReversingRescue_ReturningSuccessPartial()
        {
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockOrderStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new OrderStatus { Id = 6 });
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10, Cpf = "12312312312" } });
            _mockOrderRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<Expression<Func<Order, object>>>())).ReturnsAsync(new List<Order> { new Order { Login = "12312312312", Id = 10, ExternalOrderId = 123, User = new User { Id = 10, Cpf = "12312312312" }, Total = 50, OrderStatus = new OrderStatus { Id = 4 } } });
            _mockOrderItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderItem, bool>>>())).ReturnsAsync(new List<OrderItem> { new OrderItem { CodeItem = 123 } });
            _mockOrderReversalItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderReversalItem, bool>>>())).ReturnsAsync(new List<OrderReversalItem> { new OrderReversalItem { ExternalOrderId = 123, TotalValue = 10 } });

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var reversalRequest = new ReversalRequestDto { Cpf = "12312312312", ExternalOrderId = 123, ReversalRequestItems = new List<ReversalRequestItemsDto> { new ReversalRequestItemsDto { CodeItem = 123, ExternalOrderId = 123, TotalValue = 40 } } };

            var result = await service.DoReversal(reversalRequest);

            Assert.True(result.Success);
            Assert.IsType<List<Order>>(result.Data);
            Assert.Equal((int)OrderStatusEnum.Reversed, result.Data.First().OrderStatus.Id);
        }

        [Fact]
        public async Task DoReversal_ReversingRescueWasReversed_ReturningFalse()
        {
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockOrderStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new OrderStatus { Id = 6 });
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10, Cpf = "12312312312" } });
            _mockOrderRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, object>>>(), It.IsAny<Expression<Func<Order, object>>>())).ReturnsAsync(new List<Order> { new Order { Login = "12312312312", Id = 10, ExternalOrderId = 123, User = new User { Id = 10, Cpf = "12312312312" }, Total = 50, OrderStatus = new OrderStatus { Id = 4 } } });
            _mockOrderItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderItem, bool>>>())).ReturnsAsync(new List<OrderItem> { new OrderItem { CodeItem = 123 } });
            _mockOrderReversalItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderReversalItem, bool>>>())).ReturnsAsync(new List<OrderReversalItem> { new OrderReversalItem { CodeItem = 123 } });

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var reversalRequest = new ReversalRequestDto { Cpf = "12312312312", ExternalOrderId = 123, ReversalRequestItems = new List<ReversalRequestItemsDto> { new ReversalRequestItemsDto { CodeItem = 123, ExternalOrderId = 123, TotalValue = 50 } } };

            var result = await service.DoReversal(reversalRequest);

            Assert.False(result.Success);
            Assert.Equal("estorno recusado item já estornado", result.Message);
            
        }

        [Fact]
        public async Task DoReversal_ReversingRescue_ReturningSuccess()
        {
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockOrderStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new OrderStatus { Id = 6 });
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10, Cpf = "12312312312" } });
            _mockOrderRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order,object>>>(), It.IsAny<Expression<Func<Order, object>>>())).ReturnsAsync(new List<Order> { new Order { Login = "12312312312", Id = 10, ExternalOrderId = 123 ,User = new User { Id = 10, Cpf = "12312312312" }, Total = 50 , OrderStatus = new OrderStatus { Id = 4 } } });
            _mockOrderItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderItem, bool>>>())).ReturnsAsync(new List<OrderItem> { new OrderItem { CodeItem = 123 } });
            _mockOrderReversalItemRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<OrderReversalItem, bool>>>())).ReturnsAsync(new List<OrderReversalItem>());

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var reversalRequest = new ReversalRequestDto { Cpf = "12312312312", ExternalOrderId = 123, ReversalRequestItems = new List<ReversalRequestItemsDto> { new ReversalRequestItemsDto { CodeItem = 123, ExternalOrderId = 123, TotalValue = 50 } } };

            var result = await service.DoReversal(reversalRequest);

            Assert.True(result.Success);
            Assert.IsType<List<Order>>(result.Data);
            Assert.Equal((int)OrderStatusEnum.Reversed, result.Data.First().OrderStatus.Id);
        }

        [Fact]
        public async Task DoRescue_AuthorizationAndCreation_ReturningSuccess()
        {
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10 } });
            _mockOrderRepository.Setup(x => x.GetLast(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, int>>>())).ReturnsAsync( new Order { Id = 10, CreatedAt = DateTime.Now.AddDays(-1) });
            _mockUserPunctuationRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<UserPunctuation, bool>>>())).ReturnsAsync(new List<UserPunctuation> { new UserPunctuation { Id = 10, Punctuation = 100 } });

            var rescueRequest = new RescueRequestDto { Total = 50, RescueRequestItems = new List<RescueRequestItemsDto> { new RescueRequestItemsDto { TotalValue = 50 } } };

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var result = await service.DoRescue(rescueRequest);

            Assert.True(result.Success);
            Assert.IsType<List<Order>>(result.Data);

        }

        [Fact]
        public async Task DoRescue_AuthorizationAndCreation_NoBalance()
        {
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10 } });
            _mockOrderRepository.Setup(x => x.GetLast(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, int>>>())).ReturnsAsync(new Order { Id = 10, CreatedAt = DateTime.Now.AddDays(-1) });
            _mockUserPunctuationRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<UserPunctuation, bool>>>())).ReturnsAsync(new List<UserPunctuation> { new UserPunctuation { Id = 10, Punctuation = 45 } });

            var rescueRequest = new RescueRequestDto { Total = 50, RescueRequestItems = new List<RescueRequestItemsDto> { new RescueRequestItemsDto { TotalValue = 50 } } };

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var result = await service.DoRescue(rescueRequest);

            Assert.False(result.Success);
            Assert.Equal("Saldo insuficiente", result.Message);

        }

        [Fact]
        public async Task DoRescue_AuthorizationAndCreation_FailByTimeValidation()
        {
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>())).ReturnsAsync(new List<User> { new User { Id = 10 } });
            _mockOrderRepository.Setup(x => x.GetLast(It.IsAny<Expression<Func<Order, bool>>>(), It.IsAny<Expression<Func<Order, int>>>())).ReturnsAsync(new Order { Id = 10, CreatedAt = DateTime.Now.AddMinutes(-1) });
            _mockUserPunctuationRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<UserPunctuation, bool>>>())).ReturnsAsync(new List<UserPunctuation> { new UserPunctuation { Id = 10, Punctuation = 100 } });

            var rescueRequest = new RescueRequestDto { Total = 50, RescueRequestItems = new List<RescueRequestItemsDto> { new RescueRequestItemsDto { TotalValue = 50 } } };

            var service = new OrderService(_mockOrderReversalItemRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserRepository.Object,
                _mockLogger.Object,
                _mockOrderRepository.Object,
                _mockOrderItemRepository.Object,
                _mockOrderStatusRepository.Object,
                _mockUnitOfWork.Object,
                _mockShopUserRepository.Object,
                _mapper);

            var result = await service.DoRescue(rescueRequest);

            Assert.False(result.Success);
            Assert.Equal("suspeita de fraude por tentativa de gerar pedidos em um pequeno intervalo de tempo", result.Message);

        }

    }
}
