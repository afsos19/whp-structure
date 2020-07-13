using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NLog;
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
    public class HierarchyProcessesService : IHierarchyProcessesService, IDisposable
    {

        private readonly IHierarchyFileDataRepository _hierarchyFileDataRepository;
        private readonly IHierarchyFileDataErrorRepository _hierarchyFileDataErrorRepository;
        private readonly IHierarchyFileRepository _hierarchyFileRepository;
        private readonly IFileStatusRepository _fileStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IUserPunctuationRepository _userPunctuationRepository;
        private readonly IUserPunctuationSourceRepository _userPunctuationSourceRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOfficeRepository _officeRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly ISaleFileRepository _saleFileRepository;
        private readonly IUserStatusLogRepository _userStatusLogRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;
        private readonly IUserAccessCodeInviteRepository _userAccessCodeInviteRepository;

        public HierarchyProcessesService(ISaleFileRepository saleFileRepository, IEmailService emailService ,IUserAccessCodeInviteRepository userAccessCodeInviteRepository,IUserPunctuationSourceRepository userPunctuationSourceRepository, IUserPunctuationRepository userPunctuationRepository,IUserStatusLogRepository userStatusLogRepository, ILogger logger, IHierarchyFileDataRepository hierarchyFileDataRepository, IHierarchyFileDataErrorRepository hierarchyFileDataErrorRepository, IHierarchyFileRepository hierarchyFileRepository, IFileStatusRepository fileStatusRepository, IUserRepository userRepository, IUserStatusRepository userStatusRepository, IShopRepository shopRepository, IShopUserRepository shopUserRepository, IUnitOfWork unitOfWork, IOfficeRepository officeRepository, ISaleRepository saleRepository)
        {
            _logger = logger;
            _emailService = emailService;
            _userPunctuationRepository = userPunctuationRepository;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
            _hierarchyFileDataRepository = hierarchyFileDataRepository;
            _hierarchyFileDataErrorRepository = hierarchyFileDataErrorRepository;
            _hierarchyFileRepository = hierarchyFileRepository;
            _fileStatusRepository = fileStatusRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _shopRepository = shopRepository;
            _shopUserRepository = shopUserRepository;
            _unitOfWork = unitOfWork;
            _officeRepository = officeRepository;
            _saleRepository = saleRepository;
            _userStatusLogRepository = userStatusLogRepository;
            _userAccessCodeInviteRepository = userAccessCodeInviteRepository;
            _saleFileRepository = saleFileRepository;
        }

        public async Task ValidateImportedStructure(string pathFrom)
        {

            var ListHierarchyFileDataError = new List<HierarchyFileDataError>();
            var ListHierarchyFileData = new List<HierarchyFileData>();

            var pendingFiles = await _hierarchyFileRepository.CustomFind(x => x.FileStatus.Id == (int)FileStatusEnum.Pending, x => x.FileStatus, x => x.User);

            if (!pendingFiles.Any())
                _logger.Info($"Processamento de arquivo de hierarquia  - nenhum arquivo pendente encontrado para processar!");

            foreach (var file in pendingFiles)
            {
                var path = Path.Combine(pathFrom, file.FileName);
                var fileInfo = new FileInfo(path);

                _logger.Info($"Processamento de arquivo de hierarquia com id {file.Id} - iniciando!");

                if (File.Exists(path))
                {
                    using (var excelPackage = new ExcelPackage(fileInfo))
                    {

                        var salesmanTab = excelPackage.Workbook.Worksheets[1];
                        var managerTab = excelPackage.Workbook.Worksheets[2];

                        ListHierarchyFileDataError = new List<HierarchyFileDataError>();
                        ListHierarchyFileData = new List<HierarchyFileData>();

                        var salesmanTabResult = await ValidateSalesmanTab(file, salesmanTab);
                        var managerTabResult = await ValidateManagerTab(file, managerTab);

                        ListHierarchyFileDataError.AddRange(salesmanTabResult.errorList);
                        ListHierarchyFileDataError.AddRange(managerTabResult.errorList);

                        ListHierarchyFileData.AddRange(salesmanTabResult.dataList);
                        ListHierarchyFileData.AddRange(managerTabResult.dataList);

                        if (ListHierarchyFileDataError.Any())
                        {
                            _logger.Info($"Processamento de arquivo de hierarquia com id {file.Id} - foi encontrado erros!");
                            _hierarchyFileDataErrorRepository.SaveMany(ListHierarchyFileDataError);
                            file.FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.EndedError);
                            await _unitOfWork.CommitAsync();

                            try
                            {
                                _emailService.SendHierarchyError(file.User.Email);
                            }
                            catch
                            {
                                _logger.Warn($"Processamento de arquivo de hierarquia com id {file.Id} - não foi possivel enviar email para o usuario responsavel pelo arquivo");
                            }
                            

                        }
                        else
                        {
                            _logger.Info($"Processamento de arquivo de hierarquia com id {file.Id} - validado e alterado status para progresso");
                            _hierarchyFileDataRepository.SaveMany(ListHierarchyFileData);
                            file.FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.InProgress);
                            await _unitOfWork.CommitAsync();

                            try
                            {
                                _emailService.SendHierarchySuccess($"{(file.CurrentMonth < 10 ? "0"+file.CurrentMonth.ToString() : file.CurrentMonth.ToString())}/{file.CurrentYear}",file.User.Email);
                            }
                            catch
                            {
                                _logger.Warn($"Processamento de arquivo de hierarquia com id {file.Id} - não foi possivel enviar email para o usuario responsavel pelo arquivo");
                            }

                        }

                    }
                }
                else
                {
                    _logger.Info($"Processamento de arquivo de hierarquia com id {file.Id} - não encontrado no caminho {path}");
                }

            }

        }

        public async Task<(IEnumerable<HierarchyFileDataError> errorList, IEnumerable<HierarchyFileData> dataList)> ValidateSalesmanTab(HierarchyFile hierarchyFile, ExcelWorksheet salesmanTab)
        {
            var ListHierarchyFileDataError = new List<HierarchyFileDataError>();
            var ListHierarchyFileData = new List<HierarchyFileData>();

            for (var row = 2; row <= salesmanTab.Dimension.End.Row; row++)
            {
                if (salesmanTab.Cells[row, 1].Value != null &&
                    salesmanTab.Cells[row, 2].Value != null &&
                    salesmanTab.Cells[row, 3].Value != null &&
                    salesmanTab.Cells[row, 4].Value != null &&
                    salesmanTab.Cells[row, 5].Value != null &&
                    salesmanTab.Cells[row, 6].Value != null &&
                    salesmanTab.Cells[row, 7].Value != null)
                {
                    var hierarchyFileData = new HierarchyFileData
                    {
                        Cnpj = (salesmanTab.Cells[row, 3].Value != null) ? salesmanTab.Cells[row, 3].Value.ToString().PadLeft(14, '0').Replace("-", "").Replace("/", "").Replace(".", "").Replace(".0", "") : String.Empty,
                        Cpf = (salesmanTab.Cells[row, 4].Value != null) ? salesmanTab.Cells[row, 4].Value.ToString().PadLeft(11, '0').Replace("-", "").Replace(".", "").Replace(".0", "") : String.Empty,
                        ShopCode = (salesmanTab.Cells[row, 2].Value != null) ? salesmanTab.Cells[row, 2].Value.ToString().Replace(".0", "") : String.Empty,
                        Name = (salesmanTab.Cells[row, 5].Value != null) ? salesmanTab.Cells[row, 5].Value.ToString() : String.Empty,
                        Off = (salesmanTab.Cells[row, 7].Value != null) ? salesmanTab.Cells[row, 7].Value.ToString() : String.Empty,
                        Office = Validation.RemoverAcentos((salesmanTab.Cells[row, 6].Value != null) ? (salesmanTab.Cells[row, 6].Value.ToString().ToUpper().Trim() == "SUB GERENTE" || salesmanTab.Cells[row, 6].Value.ToString().ToUpper().Trim() == "SUB-GERENTE") ? "GERENTE" : salesmanTab.Cells[row, 6].Value.ToString() : String.Empty),
                        Resale = (salesmanTab.Cells[row, 1].Value != null) ? salesmanTab.Cells[row, 1].Value.ToString() : String.Empty,
                        Spreedsheet = "VENDEDORES_GERENTES",
                        CreatedAt = DateTime.Now,
                        HierarchyFile = hierarchyFile,
                    };

                    ListHierarchyFileDataError.AddRange(await ValidateSpreadsheetRow(hierarchyFileData, hierarchyFile, row, 1, hierarchyFileData.Spreedsheet));

                    if (ListHierarchyFileData.Any() && ListHierarchyFileData.Where(x => x.Cpf.Equals(hierarchyFileData.Cpf)).Any())
                    {
                        ListHierarchyFileDataError.Add(new HierarchyFileDataError
                        {
                            CreatedAt = DateTime.Now,
                            Description = $"CPF {hierarchyFileData.Cpf} duplicado",
                            HierarchyFile = hierarchyFile,
                            Line = row,
                            Spreedsheet = hierarchyFileData.Spreedsheet
                        });
                    }

                    ListHierarchyFileData.Add(hierarchyFileData);

                }
            }

            return (ListHierarchyFileDataError, ListHierarchyFileData);
        }

        public async Task<(IEnumerable<HierarchyFileDataError> errorList, IEnumerable<HierarchyFileData> dataList)> ValidateManagerTab(HierarchyFile hierarchyFile, ExcelWorksheet managerTab)
        {
            var ListHierarchyFileDataError = new List<HierarchyFileDataError>();
            var ListHierarchyFileData = new List<HierarchyFileData>();

            for (var row = 2; row <= managerTab.Dimension.End.Row; row++)
            {
                if (managerTab.Cells[row, 1].Value != null &&
                    managerTab.Cells[row, 2].Value != null &&
                    managerTab.Cells[row, 3].Value != null &&
                    managerTab.Cells[row, 4].Value != null &&
                    managerTab.Cells[row, 5].Value != null &&
                    managerTab.Cells[row, 6].Value != null)
                {
                    var hierarchyFileData = new HierarchyFileData
                    {
                        Cnpj = (managerTab.Cells[row, 3].Value != null) ? managerTab.Cells[row, 3].Value.ToString().PadLeft(14, '0').Replace("-", "").Replace("/", "").Replace(".", "").Replace(".0", "") : String.Empty,
                        Cpf = (managerTab.Cells[row, 4].Value != null) ? managerTab.Cells[row, 4].Value.ToString().PadLeft(11, '0').Replace("-", "").Replace(".", "").Replace(".0", "") : String.Empty,
                        ShopCode = (managerTab.Cells[row, 2].Value != null) ? managerTab.Cells[row, 2].Value.ToString().Replace(".0", "") : String.Empty,
                        Name = (managerTab.Cells[row, 5].Value != null) ? managerTab.Cells[row, 5].Value.ToString() : String.Empty,
                        Resale = (managerTab.Cells[row, 1].Value != null) ? managerTab.Cells[row, 1].Value.ToString() : String.Empty,
                        Spreedsheet = "GERENTES_REGIONAIS",
                        CreatedAt = DateTime.Now,
                        HierarchyFile = hierarchyFile,
                        Off = (managerTab.Cells[row, 6].Value != null) ? managerTab.Cells[row, 6].Value.ToString().Trim() : String.Empty,

                    };

                    ListHierarchyFileDataError.AddRange(await ValidateSpreadsheetRow(hierarchyFileData, hierarchyFile, row, 2, hierarchyFileData.Spreedsheet));

                    ListHierarchyFileData.Add(hierarchyFileData);

                }
            }

            return (ListHierarchyFileDataError, ListHierarchyFileData);
        }

        public async Task<IEnumerable<HierarchyFileDataError>> ValidateSpreadsheetRow(HierarchyFileData hierarchyFileData, HierarchyFile hierarchyFile, int line, int tab, string spreadsheet)
        {
            var ListHierarchyFileDataError = new List<HierarchyFileDataError>();

            if (String.IsNullOrEmpty(hierarchyFileData.Resale))
            {
                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Revenda em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }

            if (String.IsNullOrEmpty(hierarchyFileData.ShopCode))
            {

                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Código Loja em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }

            if (String.IsNullOrEmpty(hierarchyFileData.Cnpj))
            {

                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "CNPJ em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }
            else if (!String.IsNullOrEmpty(hierarchyFileData.Cnpj))
            {
                if (!Validation.ValidaCnpj(hierarchyFileData.Cnpj))
                {
                    ListHierarchyFileDataError.Add(new HierarchyFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = $"CNPJ {hierarchyFileData.Cnpj} inválido.",
                        HierarchyFile = hierarchyFile,
                        Line = line,
                        Spreedsheet = spreadsheet
                    });
                }
                else
                {
                    var hasCnpj = await _shopRepository.CustomFind(x => x.Cnpj.Equals(hierarchyFileData.Cnpj));
                    if (!hasCnpj.Any())
                    {
                        ListHierarchyFileDataError.Add(new HierarchyFileDataError
                        {
                            CreatedAt = DateTime.Now,
                            Description = $"CNPJ {hierarchyFileData.Cnpj} não encontrado.",
                            HierarchyFile = hierarchyFile,
                            Line = line,
                            Spreedsheet = spreadsheet
                        });
                    }
                }
            }

            if (String.IsNullOrEmpty(hierarchyFileData.Cpf))
            {
                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "CPF em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }
            else if (!String.IsNullOrEmpty(hierarchyFileData.Cpf))
            {

                if (!Validation.ValidaCPF(hierarchyFileData.Cpf))
                {
                    ListHierarchyFileDataError.Add(new HierarchyFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = "CPF inválido.",
                        HierarchyFile = hierarchyFile,
                        Line = line,
                        Spreedsheet = spreadsheet
                    });
                }
            }

            if (String.IsNullOrEmpty(hierarchyFileData.Name))
            {
                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Nome em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }
            else if (!hierarchyFileData.Name.ToCharArray().All(t => !char.IsDigit(t)))
            {

                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Nome inválido.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }


            if (String.IsNullOrEmpty(hierarchyFileData.Off))
            {

                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Desligado em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });

            }
            else if (hierarchyFileData.Off.ToUpper() != "SIM" && hierarchyFileData.Off.ToUpper() != "NÃO" && hierarchyFileData.Off.ToUpper() != "NAO")
            {

                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Desligado inválido.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });

            }

            if (tab != 1) return ListHierarchyFileDataError;

            if (String.IsNullOrEmpty(hierarchyFileData.Office))
            {

                ListHierarchyFileDataError.Add(new HierarchyFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Cargo em branco.",
                    HierarchyFile = hierarchyFile,
                    Line = line,
                    Spreedsheet = spreadsheet
                });
            }
            else
            {

                if (!Validation.IsOffice(hierarchyFileData.Office.Replace(" ", "")))
                {

                    ListHierarchyFileDataError.Add(new HierarchyFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = $"Cargo {hierarchyFileData.Office} não encontrado.",
                        HierarchyFile = hierarchyFile,
                        Line = line,
                        Spreedsheet = spreadsheet
                    });
                }

            }


            return ListHierarchyFileDataError;
        }

        public async Task<bool> ProcessesHierarchyFile()
        {
            var InProgressFiles = await _hierarchyFileRepository.CustomFind(x => x.FileStatus.Id == (int)FileStatusEnum.InProgress, x => x.FileStatus, x => x.Network);

            if (InProgressFiles.Any())
            {

                _logger.Info($" processamento da hierarquia em progresso - Iniciado ");

                foreach (var file in InProgressFiles)
                {
                    var fileSaleList = await _saleFileRepository.CustomFind(x => 
                    x.CurrentMonth == file.CurrentMonth && 
                    x.CurrentYear == file.CurrentYear && 
                    x.FileStatus.Id != (int)FileStatusEnum.EndedError &&
                    x.Network.Id == file.Network.Id, 
                    x => x.FileStatus);

                    if(fileSaleList.Any() && fileSaleList.Where(x => x.FileStatus.Id == (int)FileStatusEnum.ProcessedSales).Count() == fileSaleList.Count())
                    {
                        var dataFileList = await _hierarchyFileDataRepository.CustomFind(x => x.HierarchyFile.Id == file.Id);

                        await _shopUserRepository.ClearRegionManagerShops(dataFileList.GroupBy(x => x.Cpf).Select(x => x.Key).ToList());

                        foreach (var dataFile in dataFileList)
                        {

                            var user = (await _userRepository.CustomFind(x => x.Cpf.Equals(dataFile.Cpf), x => x.UserStatus)).FirstOrDefault();
                            var shop = (await _shopRepository.CustomFind(x => x.Cnpj.Equals(dataFile.Cnpj))).FirstOrDefault();
                            var office = string.IsNullOrEmpty(dataFile.Office) ? await _officeRepository.GetById((int)OfficeEnum.RegionManager) : (await _officeRepository.CustomFind(x => x.Description.Replace(" ", "").ToUpper().Equals(dataFile.Office.Replace(" ", "").ToUpper()))).FirstOrDefault();

                            if (user == null)
                            {

                                await CreatePreRegistrationUser(dataFile, office, shop);
                            }
                            else
                            {
                                await UpdateHierarchyUsers(user, dataFile, office, shop);
                            }

                        }

                        file.FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.SuccessfullyCompleted);
                        return await _unitOfWork.CommitAsync();
                    }

                }

            }
            else
            {
                _logger.Info($" processamento da hierarquia em progresso - não possui nenhum arquivo em progresos para processar");
                return false;
            }

            return false;

        }

        public async Task<User> UpdateHierarchyUsers(User user, HierarchyFileData hierarchyFileData, Office office, Shop shop)
        {
            user.Office = office;
            var lastSale = await _saleRepository.CustomFindLast(x => x.User.Id == user.Id, x => x.Id);
            var lastStatus = user.UserStatus;
            var userShop = await _shopUserRepository.CustomFind(x => x.User.Id == user.Id, x => x.Shop);

            if (!string.IsNullOrEmpty(hierarchyFileData.Off) && hierarchyFileData.Off.ToUpper().Equals("SIM") && user.UserStatus.Id != (int)UserStatusEnum.PreRegistration)
            {
                _logger.Info($" processamento da hierarquia em progresso - usuario com id {user.Id} foi para somente catalogo por desligado sim na planilha");
                user.OffIn = DateTime.Now;
                user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.OnlyCatalog);

            }else if (!string.IsNullOrEmpty(hierarchyFileData.Off) && hierarchyFileData.Off.ToUpper().Equals("SIM"))
            {
                _logger.Info($" processamento da hierarquia em progresso - usuario com id {user.Id} foi para inativo por desligado sim na planilha");
                user.OffIn = DateTime.Now;
                user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Inactive);
            }

            if (office.Id == (int)OfficeEnum.RegionManager && !userShop.Where(x => x.Shop.Id == shop.Id).Any())
            {
                _shopUserRepository.Save(new ShopUser
                {
                    CreatedAt = DateTime.Now,
                    Activated = true,
                    Shop = shop,
                    User = user
                });
            }
            else if (office.Id == (int)OfficeEnum.Salesman || office.Id == (int)OfficeEnum.Manager && user.UserStatus.Id != (int)UserStatusEnum.PreRegistration)
            {

                if (office.Id == (int)OfficeEnum.Salesman && lastSale != null && 
                    lastSale.CreatedAt < DateTime.Now.AddMonths(-2) && 
                    user.UserStatus.Id != (int)UserStatusEnum.FriendInvitation && 
                    user.UserStatus.Id != (int)UserStatusEnum.PreRegistration && 
                    user.UserStatus.Id != (int)UserStatusEnum.OnlyCatalog)
                {
                    _logger.Info($" processamento da hierarquia em progresso - usuario com id {user.Id} foi para somente catalogo por inatividade nas vendas");
                    user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.OnlyCatalog);
                }
                else if (office.Id == (int)OfficeEnum.Salesman && lastSale != null && lastSale.CreatedAt < DateTime.Now.AddMonths(-3) && user.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog)
                {
                    _logger.Info($" processamento da hierarquia em progresso - usuario com id {user.Id} foi inativado por inatividade nas vendas");
                    user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Inactive);
                }
                else if (
                    office.Id == (int)OfficeEnum.Salesman && lastSale != null && 
                    lastSale.CreatedAt > DateTime.Now.AddMonths(-2) && 
                    (user.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog || user.UserStatus.Id == (int)UserStatusEnum.Inactive || user.UserStatus.Id == (int)UserStatusEnum.Off) &&
                    !string.IsNullOrEmpty(hierarchyFileData.Off) &&
                    !hierarchyFileData.Off.ToUpper().Equals("SIM"))
                {
                    _logger.Info($" processamento da hierarquia em progresso - usuario com id {user.Id} foi reativado");
                    user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Active);
                }

                _shopUserRepository.DeleteMany(userShop);

                _shopUserRepository.Save(new ShopUser
                {
                    CreatedAt = DateTime.Now,
                    Activated = true,
                    Shop = shop,
                    User = user
                });

            }

            if (user.UserStatus.Id == (int)UserStatusEnum.FriendInvitation)
            {
                user.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.PreRegistration);
                user.Name = hierarchyFileData.Name;
            }
                
            
            if(user.UserStatus.Id != lastStatus.Id)
            {
                _userStatusLogRepository.Save(new UserStatusLog
                {
                    CreatedAt = DateTime.Now,
                    User = user,
                    UserStatusTo = user.UserStatus,
                    UserStatusFrom = lastStatus,
                    Description = "Processamento de hierarquia"
                });
            }

            await _unitOfWork.CommitAsync();

            return user;
        }

        public async Task<bool> CreatePreRegistrationUser(HierarchyFileData hierarchyFileData, Office office, Shop shop)
        {
            var user = new User
            {
                Activated = false,
                CreatedAt = DateTime.Now,
                Name = hierarchyFileData.Name,
                Cpf = hierarchyFileData.Cpf,
                Office = office,
                UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.PreRegistration)
            };

            _userRepository.Save(user);

            var userShop = await _shopUserRepository.CustomFind(x => x.User.Id == user.Id);

            _shopUserRepository.DeleteMany(userShop);

            _shopUserRepository.Save(new ShopUser
            {
                CreatedAt = DateTime.Now,
                Activated = true,
                Shop = shop,
                User = user
            });

            _logger.Info($" processamento da hierarquia em progresso - usuario com id {user.Cpf} foi criado em pre cadastro");

            return await _unitOfWork.CommitAsync();

        }

        public void Dispose()
        {

        }
    }
}
