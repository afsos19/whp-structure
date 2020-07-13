using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IFocusProductRepository _focusProductRepository;
        private readonly IParticipantProductRepository _participantProductRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SaleService(IUnitOfWork unitOfWork,ISaleRepository saleRepository, IFocusProductRepository focusProductRepository, IParticipantProductRepository participantProductRepository)
        {
            _saleRepository = saleRepository;
            _focusProductRepository = focusProductRepository;
            _participantProductRepository = participantProductRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Sale>> GetUserSales(int userId, int currentMonth, int currentYear) => await _saleRepository.CustomFind(x => x.User.Id == userId && x.CurrentMonth == currentMonth && x.CurrentYear == currentYear, x => x.Product, x => x.Product.CategoryProduct);

        public async Task<MyTeamSaleDto> GetTeamSales(int shop, int currentMonth, int currentYear)
        {
            var sales = await _saleRepository.CustomFind(
                x => x.Shop.Id == shop &&
                x.CurrentMonth == currentMonth &&
                x.CurrentYear == currentYear,
                x => x.Product,
                x => x.Product.CategoryProduct,
                x => x.User);

            var products = sales.Select(x => x.Product.Id).ToList();

            if (products.Count() == 0 || sales.Count() == 0)
                return new MyTeamSaleDto();

            var superTop = await _focusProductRepository.CustomFind(x => x.CurrentMonth == currentMonth && x.CurrentYear == currentYear);
            var participant = await _participantProductRepository.CustomFind(x => x.CurrentMonth == currentMonth && x.CurrentYear == currentYear);

            var salesGroup = sales.GroupBy(x => new { x.User, x.Product.Id })
                .Select(
                x => new MyTeamSaleReturnListDto
                {
                    Name = x.Key.User.Name,
                    IdProduct = x.Key.Id,
                    SuperTop = 0,
                    Participant = 0
                }).ToList();

            foreach (var item in salesGroup)
            {
                item.SuperTop = superTop.Where(p => p.Product.Id == item.IdProduct).Count();
                item.Participant = participant.Where(p => p.Product.Id == item.IdProduct).Count();
            }

            salesGroup = salesGroup.GroupBy(x => new { x.Name, x.SuperTop, x.Participant })
                .Select(
                x => new MyTeamSaleReturnListDto
                {
                    Name = x.Key.Name,
                    SuperTop = x.Sum(p => p.SuperTop),
                    Participant = x.Sum(p => p.Participant)
                }).ToList();

            var superTopCount = superTop.Count(x => products.Contains(x.Product.Id) && x.CurrentMonth == currentMonth && x.CurrentYear == currentYear);
            var participantCount = participant.Count(x => products.Contains(x.Product.Id) && x.CurrentMonth == currentMonth && x.CurrentYear == currentYear);

            return new MyTeamSaleDto
            {
                ListSale = salesGroup,
                SuperTop = (superTopCount * 100) / products.Count(),
                Participant = (participantCount * 100) / products.Count()
            };
        }

        public async Task<(bool Approved, string message)> ApproveSaleToProcessing(int currentMonth, int currentYear)
        {
            var sales = await _saleRepository.CustomFind(x => !x.Activated &&
            !x.Processed &&
            x.CurrentMonth == currentMonth &&
            x.CurrentYear == currentYear);

            if (!sales.Any())
                return (false, $"Nenhum registro encontrado para aprovação no mes {currentMonth} e ano {currentYear}");

            foreach(var item in sales)
                item.Activated = true;

            await _unitOfWork.CommitAsync();

            return (true, $"Aprovação realizada com sucesso no mes {currentMonth} e ano {currentYear}");

        }
    }
}
