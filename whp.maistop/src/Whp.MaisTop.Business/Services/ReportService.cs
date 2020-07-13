using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Business.Services
{
    public class ReportService : IReportService
    {

        private readonly ISaleFileDataRepository _saleFileDataRepository;
        private readonly ISaleFileRepository _saleFileRepository;
        private readonly IHierarchyFileRepository _hierarchyFileRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFocusProductService _focusProductService;
        private readonly IParticipantProductService _participantProductService;
        private readonly INetworkRepository _networkRepository;
        private readonly ISaleRepository _saleRepository;

        public ReportService(ISaleRepository saleRepository, INetworkRepository networkRepository, IHierarchyFileRepository hierarchyFileRepository, ISaleFileDataRepository saleFileDataRepository, ISaleFileRepository saleFileRepository, IProductRepository productRepository, IUserRepository userRepository, IFocusProductService focusProductService, IParticipantProductService participantProductService)
        {
            _saleRepository = saleRepository;
            _saleFileDataRepository = saleFileDataRepository;
            _hierarchyFileRepository = hierarchyFileRepository;
            _saleFileRepository = saleFileRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _focusProductService = focusProductService;
            _participantProductService = participantProductService;
            _networkRepository = networkRepository;
        }

        public async Task<(bool hasReport, object report, string message)> GetHierarchyFile(FilterBaseTrackingDto filterDto)
        {
            var result = await _hierarchyFileRepository.GetHierarchyFile(filterDto.Start, filterDto.Length, filterDto.CurrentMonth, filterDto.CurrentYear, filterDto.Network, filterDto.FileStatusId);

            if (!result.Rows.Any())
                return (false, null, "Nenhum registro encontrado");

            var report = new
            {
                data = result.Rows.Select(x =>
                new[] {
                    x.User.Name,
                    x.Network.Name,
                    x.FileStatus.Description,
                    $"https://programamaistop.com.br/api/wwwroot/Content/Spreadsheet/Hierarchy/{x.FileName}",
                    $"{x.CurrentMonth.ToString()}/{x.CurrentYear.ToString()}",
                    x.CreatedAt.ToString("dd/MM/yyyy")
                }).ToArray(),
                recordsTotal = result.Count,
                recordsFiltered = result.Count
            };

            return (true, report, "Registros encontrado com sucesso!");
        }

        public async Task<(bool hasReport, object report, string message)> GetSaleFile(FilterBaseTrackingDto filterDto)
        {
            var result = await _saleFileRepository.GetSaleFile(filterDto.Start, filterDto.Length, filterDto.CurrentMonth, filterDto.CurrentYear, filterDto.Network, filterDto.FileStatusId);

            if (!result.Rows.Any())
                return (false, null, "Nenhum registro encontrado");

            var report = new
            {
                data = result.Rows.Select(x =>
                new[] {
                    x.User.Name,
                    x.Network.Name,
                    x.FileStatus.Description,
                    $"https://programamaistop.com.br/api/wwwroot/Content/Spreadsheet/Sale/{x.FileName}",
                    $"{x.CurrentMonth.ToString()}/{x.CurrentYear.ToString()}",
                    x.CreatedAt.ToString("dd/MM/yyyy")
                }).ToArray(),
                recordsTotal = result.Count,
                recordsFiltered = result.Count
            };

            return (true, report, "Registros encontrado com sucesso!");
        }

        public async Task<(bool hasReport, object report, string message)> PreProcessingSales(FilterDto filterDto)
        {

            if ((await _saleFileDataRepository.CustomFindCount(x =>
                x.SaleFile.CurrentMonth == filterDto.CurrentMonth &&
                x.SaleFile.CurrentYear == filterDto.CurrentYear &&
                (filterDto.Network > 0 ? x.SaleFile.Network.Id == filterDto.Network : 1 == 1) &&
                x.SaleFile.FileStatus.Id == (int)FileStatusEnum.InProgress)) == 0)
                return (false, null, $"Nenhum arquivo de venda encontrado no mes {filterDto.CurrentMonth} e ano {filterDto.CurrentYear}");

            if ((await _saleFileDataRepository.CustomFindCount(x =>
                x.SaleFile.CurrentMonth == filterDto.CurrentMonth &&
                x.SaleFile.CurrentYear == filterDto.CurrentYear &&
                x.SaleFileSkuStatus.Id != (int)FileSKUStatusEnum.AutomaticValidate &&
                x.SaleFileSkuStatus.Id != (int)FileSKUStatusEnum.Validated &&
                x.SaleFileSkuStatus.Id != (int)FileSKUStatusEnum.NotValidated &&
                (filterDto.Network > 0 ? x.SaleFile.Network.Id == filterDto.Network : 1 == 1) &&
                x.SaleFile.FileStatus.Id == (int)FileStatusEnum.InProgress)) > 0)
                return (false, null, "Existem linhas de vendas aguardando classificação");

            if ((await _saleFileDataRepository.CustomFindCount(x =>
                x.SaleFile.CurrentMonth == filterDto.CurrentMonth &&
                x.SaleFile.CurrentYear == filterDto.CurrentYear &&
                x.SaleFileSkuStatus.Id == (int)FileSKUStatusEnum.AutomaticValidate &&
                x.SaleFileSkuStatus.Id == (int)FileSKUStatusEnum.Validated &&
                x.Product == 0 &&
                (filterDto.Network > 0 ? x.SaleFile.Network.Id == filterDto.Network : 1 == 1) &&
                x.SaleFile.FileStatus.Id == (int)FileStatusEnum.InProgress)) > 0)
                return (false, null, "Existem linhas de vendas com classificações incorretas");

            var result = await _saleFileDataRepository.GetPreProcessingSale(filterDto.CurrentYear, filterDto.CurrentMonth, filterDto.Start, filterDto.Length, filterDto.Network);

            var report = new
            {
                data = result.Data,
                recordsTotal = result.Count,
                recordsFiltered = result.Count
            };

            return (true, report, "Relatório gerado com sucesso!");
        }

        public async Task<(bool hasReport, object report, string message)> PreProcessingSalesPunctuation(FilterDto filterDto)
        {
            var sales = await _saleRepository.CustomFind(x => !x.Activated && 
            !x.Processed && 
            x.CurrentMonth == filterDto.CurrentMonth &&
            x.CurrentYear == filterDto.CurrentYear,
            x => x.Network);

            if (!sales.Any())
                return (false, null, "Nenhum registro encontrado no parametro informado");

            var networks = await _networkRepository.GetAll();
            networks = networks.OrderBy(x => x.Name);

            var report = networks.Select(x => new
            {
                Network = x.Name,
                Salesman = sales.Where(s => s.Network.Id == x.Id).Sum(s => s.Punctuation),
                RegionManager = sales.Where(s => s.Network.Id == x.Id).Sum(s => s.Punctuation) * (decimal)0.05,
                Manager = sales.Where(s => s.Network.Id == x.Id).Sum(s => s.Punctuation) * (decimal)0.1,
                Total = sales.Where(s => s.Network.Id == x.Id).Sum(s => s.Punctuation) + (sales.Where(s => s.Network.Id == x.Id).Sum(s => s.Punctuation) * (decimal)0.05) + (sales.Where(s => s.Network.Id == x.Id).Sum(s => s.Punctuation) * (decimal)0.1)
            }).ToList();

            return (true, report, "Relatório gerado com sucesso");

        }
    }
}
