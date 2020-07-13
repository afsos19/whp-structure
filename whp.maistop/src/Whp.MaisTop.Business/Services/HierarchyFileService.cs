using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class HierarchyFileService : IHierarchyFileService
    {
        private readonly IMapper _mapper;
        private readonly IHierarchyFileDataRepository _hierarchyFileDataRepository;
        private readonly IHierarchyFileDataErrorRepository _hierarchyFileDataErrorRepository;
        private readonly IHierarchyFileRepository _hierarchyFileRepository;
        private readonly IFileStatusRepository _fileStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly INetworkRepository _networkRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IHostingEnvironment _env;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfficeRepository _officeRepository;
        private readonly ISaleRepository _saleRepository;

        public HierarchyFileService(IMapper mapper, IHierarchyFileDataRepository hierarchyFileDataRepository, IHierarchyFileDataErrorRepository hierarchyFileDataErrorRepository, IHierarchyFileRepository hierarchyFileRepository, IFileStatusRepository fileStatusRepository, IUserRepository userRepository, IUserStatusRepository userStatusRepository, INetworkRepository networkRepository, IShopRepository shopRepository, IShopUserRepository shopUserRepository, IHostingEnvironment env, IUnitOfWork unitOfWork, IOfficeRepository officeRepository, ISaleRepository saleRepository)
        {
            _mapper = mapper;
            _hierarchyFileDataRepository = hierarchyFileDataRepository;
            _hierarchyFileDataErrorRepository = hierarchyFileDataErrorRepository;
            _hierarchyFileRepository = hierarchyFileRepository;
            _fileStatusRepository = fileStatusRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _networkRepository = networkRepository;
            _shopRepository = shopRepository;
            _shopUserRepository = shopUserRepository;
            _env = env;
            _unitOfWork = unitOfWork;
            _officeRepository = officeRepository;
            _saleRepository = saleRepository;
        }
        
        public async Task<object> GetFileStatus(FileStatusParamDto fileStatusParamDto, int network)
        {

            var hierarchyFile = (await _hierarchyFileRepository.CustomFind(
                x => x.CurrentMonth == fileStatusParamDto.CurrentMonth &&
                x.Network.Id == network &&
                x.CurrentYear == fileStatusParamDto.CurrentYear,
                x => x.FileStatus));

            if (!hierarchyFile.Any())
                return null;

            var hierarchyFileObj = hierarchyFile.OrderBy(x => x.Id).Last();

            if (hierarchyFile != null && hierarchyFileObj.FileStatus.Id == (int)FileStatusEnum.EndedError) {
                
                var saleFileDataError = await _hierarchyFileDataErrorRepository.CustomFind(x => x.HierarchyFile.Id == hierarchyFileObj.Id);
                return new { hierarchyFileObj.FileStatus , saleFileDataError };
            }
                

            return new { hierarchyFileObj.FileStatus };
        }

        public async Task<HierarchyFileData> GetFileDataByCnpj(string cnpj) => (await _hierarchyFileDataRepository.CustomFind(x => x.Cnpj == cnpj)).FirstOrDefault();

        public async Task<IEnumerable<string>> SendHierarchyFile(FileStatusParamDto fileStatusParamDto, IFormFile formFile, int network, int user)
        {

            var extensions = Path.GetFileName(formFile.FileName).Split('.').Last().ToUpper();

            if (extensions == "XLS")
                return new string[1] { "favor importar arquivo com extensão .xlsx." };

            if (extensions != "XLSX")
                return new string[1] { "favor importar arquivo com extensão .xlsx." };

            if (await _hierarchyFileRepository.ExistFile(fileStatusParamDto.CurrentMonth, fileStatusParamDto.CurrentYear, network))
                return new string[1] { "Existe um arquivo em fase de processamento ou ja foi processado" };

            var fileName = $"{DateTime.Now.Year}" +
                        $"{DateTime.Now.Month.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Day.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Hour.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Minute.ToString().PadLeft(2, '0')}" +
                        $"{DateTime.Now.Second.ToString().PadLeft(2, '0')}.{extensions}";

            var path = Path.Combine(_env.WebRootPath, $"Content/SpreadSheet/Hierarchy/{Path.GetFileName(fileName)}");

            if (File.Exists(path))
                File.Delete(path);

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            var spreadSheetMessages = new List<string>();
            var columnsSalesManager = new string[7] { "REVENDA", "COD LOJA", "CNPJ", "CPF", "NOME", "CARGO", "DESLIGADO" };
            var columnsRegionManager = new string[6] { "REVENDA", "COD LOJA", "CNPJ", "CPF", "NOME", "DESLIGADO" };
            var fileInfo = new FileInfo(path);

            using (var excelPackage = new ExcelPackage(fileInfo))
            {
                var tabSalesManager = excelPackage.Workbook.Worksheets[1];
                var tabRegionManager = excelPackage.Workbook.Worksheets[2];

                spreadSheetMessages.AddRange(FileManipulator.ValidateSpreadsheet(tabSalesManager, columnsSalesManager));
                spreadSheetMessages.AddRange(FileManipulator.ValidateSpreadsheet(tabRegionManager, columnsRegionManager));

            }

            if (spreadSheetMessages.Count == 0)
            {
                _hierarchyFileRepository.Save(new HierarchyFile
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
        
    }
}
