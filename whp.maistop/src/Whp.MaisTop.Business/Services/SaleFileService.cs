using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Utils;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class SaleFileService : ISaleFileService
    {
        private readonly IMapper _mapper;
        private readonly ISaleProcessesService _saleProcessesService;
        private readonly ISaleFileDataErrorRepository _saleFileDataErrorRepository;
        private readonly ISaleFileDataRepository _saleFileDataRepository;
        private readonly ISaleFileRepository _saleFileRepository;
        private readonly IFileStatusRepository _fileStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly INetworkRepository _networkRepository;
        private readonly IHostingEnvironment _env;
        private readonly IShopRepository _shopRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFocusProductRepository _focusProductRepository;
        private readonly IParticipantProductRepository _participantProductRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleFileSkuStatusRepository _saleFileSkuStatusRepository;
        private readonly ICategoryProductRepository _categoryProductRepository;

        public SaleFileService(IMapper mapper, ISaleProcessesService saleProcessesService, ISaleFileDataErrorRepository saleFileDataErrorRepository, ISaleFileDataRepository saleFileDataRepository, ISaleFileRepository saleFileRepository, IFileStatusRepository fileStatusRepository, IUserRepository userRepository, IUserStatusRepository userStatusRepository, INetworkRepository networkRepository, IHostingEnvironment env, IShopRepository shopRepository, IShopUserRepository shopUserRepository, IProductRepository productRepository, IFocusProductRepository focusProductRepository, IParticipantProductRepository participantProductRepository, IOfficeRepository officeRepository, IUnitOfWork unitOfWork, ISaleRepository saleRepository, ISaleFileSkuStatusRepository saleFileSkuStatusRepository, ICategoryProductRepository categoryProductRepository)
        {
            _mapper = mapper;
            _saleProcessesService = saleProcessesService;
            _saleFileDataErrorRepository = saleFileDataErrorRepository;
            _saleFileDataRepository = saleFileDataRepository;
            _saleFileRepository = saleFileRepository;
            _fileStatusRepository = fileStatusRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _networkRepository = networkRepository;
            _env = env;
            _shopRepository = shopRepository;
            _shopUserRepository = shopUserRepository;
            _productRepository = productRepository;
            _focusProductRepository = focusProductRepository;
            _participantProductRepository = participantProductRepository;
            _officeRepository = officeRepository;
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _saleFileSkuStatusRepository = saleFileSkuStatusRepository;
            _categoryProductRepository = categoryProductRepository;
        }

        public async Task<object> GetFileStatus(FileStatusParamDto fileStatusParamDto, int network)
        {
            var saleFile = (await _saleFileRepository.CustomFind(
                x => x.CurrentMonth == fileStatusParamDto.CurrentMonth &&
                x.Network.Id == network &&
                x.CurrentYear == fileStatusParamDto.CurrentYear,
                x => x.FileStatus));

            if (!saleFile.Any())
                return null;

            var saleFileObj = saleFile.OrderBy(x => x.Id).Last();

            if (saleFileObj.FileStatus.Id == (int)FileStatusEnum.InProgress)
            {
                var saleFileData = await _saleFileDataRepository.CustomFind(x => x.SaleFile.Id == saleFileObj.Id,
                x => x.SaleFileSkuStatus);

                return new { saleFileData, saleFileObj.FileStatus };

            }
            else if (saleFileObj.FileStatus.Id == (int)FileStatusEnum.EndedError)
            {
                var saleFileDataError = await _saleFileDataErrorRepository.CustomFind(x => x.SaleFile.Id == saleFileObj.Id);

                return new { saleFileDataError, saleFileObj.FileStatus };
            }
            else
            {
                return new { saleFileObj.FileStatus };
            }

        }

        public async Task<IEnumerable<string>> SendSaleFile(FileStatusParamDto fileStatusParamDto, IFormFile formFile, int network, int user)
        {

            var extensions = Path.GetFileName(formFile.FileName).Split('.').Last().ToUpper();

            if (extensions == "XLS")
                return new string[1] { "favor importar arquivo com extensão .xlsx." };

            if (extensions != "XLSX")
                return new string[1] { "favor importar arquivo com extensão .xlsx." };

            if (await _saleFileRepository.ExistFile(fileStatusParamDto.CurrentMonth, fileStatusParamDto.CurrentYear, network))
                return new string[1] { "Existe um arquivo em fase de processamento ou ja foi processado" };

            var fileName = $"{DateTime.Now.Year}" +
                        $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

            var path = Path.Combine(_env.WebRootPath, $"Content/SpreadSheet/Sale/{Path.GetFileName(fileName)}");

            if (File.Exists(path))
                File.Delete(path);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var spreadSheetMessages = new List<string>();
            var columnsToValidate = new string[11] { "REVENDA", "COD LOJA", "CNPJ", "CPF VENDEDOR", "NOME VENDEDOR", "CATEGORIA", "DESCRIÇÃO PRODUTO", "QUANTIDADE", "DATA VENDA", "NÚMERO PEDIDO", "CÓDIGO EAN" };
            var fileInfo = new FileInfo(path);

            using (var excelPackage = new ExcelPackage(fileInfo))
            {
                var tab = excelPackage.Workbook.Worksheets[1];

                spreadSheetMessages = FileManipulator.ValidateSpreadsheet(tab, columnsToValidate);

            }

            if (spreadSheetMessages.Count == 0)
            {
                _saleFileRepository.Save(new SaleFile
                {
                    CreatedAt = DateTime.Now,
                    CurrentMonth = fileStatusParamDto.CurrentMonth,
                    CurrentYear = fileStatusParamDto.CurrentYear,
                    FileName = fileName,
                    FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.Pending),
                    Network = await _networkRepository.GetById(network),
                    User = await _userRepository.GetById(user)
                });

                await _unitOfWork.CommitAsync();

                spreadSheetMessages.Add("Carga recebida com sucesso. Aguarde a validação dos seus dados.");
            }

            return spreadSheetMessages;
        }

        public async Task<object> GetPendingClassification(FilterDto filterDto)
        {

            var result = await _saleFileDataRepository.GetPendingClassification(filterDto.Start, filterDto.Length, filterDto.Network);

            var report = new
            {
                data = result.Data.Select(x =>
                new[] {
                    x.StatusSKU,
                    x.ProductDescription,
                    $"{x.CurrentMonth.ToString()}/{x.CurrentYear.ToString()}",
                    x.Resale,
                    x.Network.ToString(),
                }).ToArray(),
                recordsTotal = result.Count,
                recordsFiltered = result.Count
            };
            return report;
        }

        public async Task<string> UpdateSaleFileDataSku(SaleFileDataDto saleFileDataDtos)
        {
            var saleFileDataEntityList = await _saleFileDataRepository.CustomFind(x => x.ProductDescription == saleFileDataDtos.ProductDescription,
                x => x.SaleFileSkuStatus,
                x => x.SaleFile,
                x => x.SaleFile.FileStatus);

            var messageReturn = "";

            if (!saleFileDataDtos.NotExisting)
            {
                messageReturn = $"Linhas de arquivo de venda dados com id {saleFileDataDtos.Id} validada com o produto {saleFileDataDtos.Product}!";

                foreach(var item in saleFileDataEntityList)
                {
                    item.Product = saleFileDataDtos.Product;
                    item.SaleFileSkuStatus = await _saleFileSkuStatusRepository.GetById((int)FileSKUStatusEnum.Validated);
                }

            }
            else
            {
                messageReturn = $"Linhas de arquivo de venda dados com id {saleFileDataDtos.Id} foi inválidada!";
                foreach(var item in saleFileDataEntityList)
                {
                    item.SaleFileSkuStatus = await _saleFileSkuStatusRepository.GetById((int)FileSKUStatusEnum.NotValidated);
                }
                
            }

            await _unitOfWork.CommitAsync();
            return messageReturn;
        }

        public async Task<bool> DoApprove(FilterDto filterDto)
        {
            var files = await _saleFileRepository.CustomFind(x =>
            x.CurrentMonth == filterDto.CurrentMonth &&
            x.CurrentYear == filterDto.CurrentYear &&
            (filterDto.Network > 0 ? x.Network.Id == filterDto.Network : 1 == 1) &&
            x.FileStatus.Id == (int)FileStatusEnum.InProgress);

            var fileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.SuccessfullyCompleted);

            if (!files.Any())
                return false;


            foreach(var item in files)
            {
                item.FileStatus = fileStatus;
            }


            return await _unitOfWork.CommitAsync();

        }

    }
}
