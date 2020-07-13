using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using NLog;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public class SaleProcessesService : ISaleProcessesService
    {

        private readonly ISaleFileDataErrorRepository _saleFileDataErrorRepository;
        private readonly ISaleFileDataRepository _saleFileDataRepository;
        private readonly ISaleFileRepository _saleFileRepository;
        private readonly IFileStatusRepository _fileStatusRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly INetworkRepository _networkRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFocusProductRepository _focusProductRepository;
        private readonly IParticipantProductRepository _participantProductRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleRepository _saleRepository;
        private readonly ILogger _logger;
        private readonly IEmailService _emailService;
        private readonly ISaleFileSkuStatusRepository _saleFileSkuStatusRepository;
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IUserPunctuationRepository _userPunctuationRepository;
        private readonly IUserPunctuationSourceRepository _userPunctuationSourceRepository;

        public SaleProcessesService(IEmailService emailService, ISaleFileDataErrorRepository saleFileDataErrorRepository, ISaleFileDataRepository saleFileDataRepository, ISaleFileRepository saleFileRepository, IFileStatusRepository fileStatusRepository, IUserRepository userRepository, IUserStatusRepository userStatusRepository, INetworkRepository networkRepository, IShopRepository shopRepository, IShopUserRepository shopUserRepository, IProductRepository productRepository, IFocusProductRepository focusProductRepository, IParticipantProductRepository participantProductRepository, IOfficeRepository officeRepository, IUnitOfWork unitOfWork, ISaleRepository saleRepository, ILogger logger, ISaleFileSkuStatusRepository saleFileSkuStatusRepository, ICategoryProductRepository categoryProductRepository, IUserPunctuationRepository userPunctuationRepository, IUserPunctuationSourceRepository userPunctuationSourceRepository)
        {
            _saleFileDataErrorRepository = saleFileDataErrorRepository;
            _emailService = emailService;
            _saleFileDataRepository = saleFileDataRepository;
            _saleFileRepository = saleFileRepository;
            _fileStatusRepository = fileStatusRepository;
            _userRepository = userRepository;
            _userStatusRepository = userStatusRepository;
            _networkRepository = networkRepository;
            _shopRepository = shopRepository;
            _shopUserRepository = shopUserRepository;
            _productRepository = productRepository;
            _focusProductRepository = focusProductRepository;
            _participantProductRepository = participantProductRepository;
            _officeRepository = officeRepository;
            _unitOfWork = unitOfWork;
            _saleRepository = saleRepository;
            _logger = logger;
            _saleFileSkuStatusRepository = saleFileSkuStatusRepository;
            _categoryProductRepository = categoryProductRepository;
            _userPunctuationRepository = userPunctuationRepository;
            _userPunctuationSourceRepository = userPunctuationSourceRepository;
        }

        public async Task ValidateImportedStructure(string pathFrom)
        {

            var ListSaleFileDataError = new List<SaleFileDataError>();
            var ListSaleFileData = new List<SaleFileData>();

            var pendingFiles = await _saleFileRepository.CustomFind(x => x.FileStatus.Id == (int)FileStatusEnum.Pending, x => x.FileStatus, x => x.Network, x => x.User);

            if (!pendingFiles.Any())
                _logger.Info($"Processamento de arquivo de vendas  - nenhum arquivo pendente encontrado para processar!");

            foreach (var file in pendingFiles)
            {

                _logger.Info($"Processamento de arquivo de vendas com id {file.Id} - iniciando!");

                var path = Path.Combine(pathFrom, file.FileName);
                var fileInfo = new FileInfo(path);

                if (File.Exists(path))
                {

                    using (var excelPackage = new ExcelPackage(fileInfo))
                    {

                        ListSaleFileDataError = new List<SaleFileDataError>();
                        ListSaleFileData = new List<SaleFileData>();

                        var saleTab = excelPackage.Workbook.Worksheets[1];

                        var saleTabResult = await ValidateSaleTab(file, saleTab);

                        ListSaleFileDataError.AddRange(saleTabResult.errorList);

                        ListSaleFileData.AddRange(saleTabResult.dataList);

                        if (saleTabResult.errorList.Any())
                        {
                            _logger.Info($"Processamento de arquivo de vendas com id {file.Id} - encontrado erros de validação!");
                            _saleFileDataErrorRepository.SaveMany(ListSaleFileDataError);
                            file.FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.EndedError);
                            await _unitOfWork.CommitAsync();
                        }
                        else if (saleTabResult.dataList.Any())
                        {
                            var EntityFile = saleTabResult.dataList.First().SaleFile;

                            if (ListSaleFileDataError.Any())
                            {
                                _logger.Info($"Processamento de arquivo de vendas com id {file.Id} - encontrado erros de validação!");
                                _saleFileDataErrorRepository.SaveMany(ListSaleFileDataError);
                                EntityFile.FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.EndedError);
                                await _unitOfWork.CommitAsync();

                                try
                                {
                                    _emailService.SendSaleError(file.User.Email);
                                }
                                catch
                                {
                                    _logger.Warn($"Processamento de arquivo de venda com id {file.Id} - não foi possivel enviar email para o usuario responsavel pelo arquivo");
                                }

                            }
                            else
                            {
                                _saleFileDataRepository.SaveMany(ListSaleFileData);

                                _logger.Info($"Processamento de arquivo de vendas com id {file.Id} - processamento em progresso, existe validacoes de sku pendente!");
                                EntityFile.FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.InProgress);
                                await _unitOfWork.CommitAsync();

                                try
                                {
                                    _emailService.SendSaleSuccess($"{(file.CurrentMonth < 10 ? "0" + file.CurrentMonth.ToString() : file.CurrentMonth.ToString())}/{file.CurrentYear}", file.User.Email);
                                    _emailService.SendSKUEnabled(file.Network.Name);
                                }
                                catch
                                {
                                    _logger.Warn($"Processamento de arquivo de venda com id {file.Id} - não foi possivel enviar email para o usuario responsavel pelo arquivo");
                                }


                            }
                        }
                        else
                        {
                            _logger.Info($"Processamento de arquivo de vendas com id {file.Id} - erro!");
                        }
                    }
                }
                else
                {
                    _logger.Warn($"Processamento de arquivo de vendas com id {file.Id} - arquivo não encontrado!");
                }

            }


        }

        private async Task<(IEnumerable<SaleFileDataError> errorList, IEnumerable<SaleFileData> dataList)> ValidateSaleTab(SaleFile saleFile, ExcelWorksheet saleTab)
        {

            var ListSaleFileDataError = new List<SaleFileDataError>();
            var ListSaleFileData = new List<SaleFileData>();


            for (var row = 2; row <= saleTab.Dimension.End.Row; row++)
            {
                if (saleTab.Cells[row, 1].Value != null &&
                    saleTab.Cells[row, 2].Value != null &&
                    saleTab.Cells[row, 3].Value != null &&
                    saleTab.Cells[row, 4].Value != null &&
                    saleTab.Cells[row, 5].Value != null &&
                    saleTab.Cells[row, 7].Value != null &&
                    saleTab.Cells[row, 8].Value != null &&
                    saleTab.Cells[row, 9].Value != null &&
                    saleTab.Cells[row, 10].Value != null &&
                    saleTab.Cells[row, 11].Value != null)
                {
                    var saleFileData = new SaleFileData
                    {

                        ShopCode = (saleTab.Cells[row, 2].Value != null) ? saleTab.Cells[row, 2].Value.ToString().Replace(".0", "") : String.Empty,
                        Cnpj = (saleTab.Cells[row, 3].Value != null) ? saleTab.Cells[row, 3].Value.ToString().PadLeft(14, '0').Replace("-", "").Replace("/", "").Replace(".", "").Replace(".0", "") : String.Empty,
                        CpfSalesman = (saleTab.Cells[row, 4].Value != null) ? saleTab.Cells[row, 4].Value.ToString().PadLeft(11, '0').Replace("-", "").Replace(".", "").Replace(".0", "").Trim() : String.Empty,
                        RequestNumber = (saleTab.Cells[row, 10].Value != null) ? saleTab.Cells[row, 10].Value.ToString().Replace(".0", "") : String.Empty,
                        SaleDate = (saleTab.Cells[row, 9].Value != null) && Validation.CheckValidDateFromBr(saleTab.Cells[row, 9].Value.ToString()) ? new DateTime(int.Parse(saleTab.Cells[row, 9].Value.ToString().Split('/')[2]), int.Parse(saleTab.Cells[row, 9].Value.ToString().Split('/')[1]), int.Parse(saleTab.Cells[row, 9].Value.ToString().Split('/')[0])) : DateTime.MinValue,
                        ProductDescription = (saleTab.Cells[row, 7].Value != null) ? saleTab.Cells[row, 7].Value.ToString().Replace("'", "").Substring(0, saleTab.Cells[row, 7].Value.ToString().Length - 1) : String.Empty,
                        Amount = (saleTab.Cells[row, 8].Value != null) ? Convert.ToInt16(saleTab.Cells[row, 8].Value.ToString().Replace(".0", "")) : 0,
                        Category = (saleTab.Cells[row, 6].Value != null) ? saleTab.Cells[row, 6].Value.ToString() : String.Empty,
                        NameSalesman = (saleTab.Cells[row, 5].Value != null) ? saleTab.Cells[row, 5].Value.ToString() : String.Empty,
                        Resale = (saleTab.Cells[row, 1].Value != null) ? saleTab.Cells[row, 1].Value.ToString() : String.Empty,
                        CreatedAt = DateTime.Now,
                        SaleFile = saleFile,
                        SaleFileSkuStatus = await _saleFileSkuStatusRepository.GetById((int)FileSKUStatusEnum.PendingClassification),
                        Product = 0,
                        EanCode = (saleTab.Cells[row, 11].Value != null) ? saleTab.Cells[row, 11].Value.ToString() : String.Empty,

                    };

                    ListSaleFileDataError.AddRange(await ValidateSpreadsheetRow(saleFileData, saleFile, row, saleFile.CurrentMonth));

                    ListSaleFileData.Add(saleFileData);

                }
                else
                {
                    ListSaleFileDataError.Add(new SaleFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = "Possui uma coluna não preenchida",
                        SaleFile = saleFile,
                        Line = row
                    });
                }
            }


            return (ListSaleFileDataError, ListSaleFileData);
        }

        private async Task<IEnumerable<SaleFileDataError>> ValidateSpreadsheetRow(SaleFileData saleFileData, SaleFile saleFile, int line, int month)
        {
            var ListSaleFileDataError = new List<SaleFileDataError>();

            if (!string.IsNullOrEmpty(saleFileData.EanCode))
            {
                var network = (await _shopUserRepository.CustomFind(x => x.Shop.Cnpj.Equals(saleFileData.Cnpj), x => x.Shop)).FirstOrDefault();

                if (network != null)
                {
                    var product = (await _productRepository.CustomFind(x => x.Ean.Equals(saleFileData.EanCode))).FirstOrDefault();
                    if (product != null)
                    {
                        saleFileData.Product = product.Id;
                        saleFileData.SaleFileSkuStatus = await _saleFileSkuStatusRepository.GetById((int)FileSKUStatusEnum.AutomaticValidate);
                    }
                }

            }

            if (String.IsNullOrEmpty(saleFileData.ProductDescription))
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Código produto em branco.",
                    SaleFile = saleFile,
                    Line = line
                });
            }

            if (String.IsNullOrEmpty(saleFileData.Resale))
            {
                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Revenda em branco.",
                    SaleFile = saleFile,
                    Line = line
                });
            }

            if (String.IsNullOrEmpty(saleFileData.ShopCode) || saleFileData.ShopCode == "0")
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Código Loja em branco.",
                    SaleFile = saleFile,
                    Line = line
                });
            }

            if (Convert.ToDateTime(saleFileData.SaleDate) == DateTime.MinValue)
            {
                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Data inválida.",
                    SaleFile = saleFile,
                    Line = line
                });
            }
            else if (Convert.ToDateTime(saleFileData.SaleDate).Month != month)
            {
                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Data fora do período informado.",
                    SaleFile = saleFile,
                    Line = line
                });
            }

            if (String.IsNullOrEmpty(saleFileData.RequestNumber))
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Número Pedido em branco.",
                    SaleFile = saleFile,
                    Line = line
                });
            }



            if (String.IsNullOrEmpty(saleFileData.Cnpj))
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "CNPJ em branco.",
                    SaleFile = saleFile,
                    Line = line,
                });
            }
            else if (!String.IsNullOrEmpty(saleFileData.Cnpj))
            {
                if (!Validation.ValidaCnpj(saleFileData.Cnpj))
                {
                    ListSaleFileDataError.Add(new SaleFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = $"CNPJ {saleFileData.Cnpj} inválido.",
                        SaleFile = saleFile,
                        Line = line
                    });
                }
                else
                {
                    var hasCnpj = await _shopRepository.CustomFind(x => x.Cnpj.Equals(saleFileData.Cnpj));
                    if (!hasCnpj.Any())
                    {
                        ListSaleFileDataError.Add(new SaleFileDataError
                        {
                            CreatedAt = DateTime.Now,
                            Description = $"CNPJ {saleFileData.Cnpj} não encontrado.",
                            SaleFile = saleFile,
                            Line = line
                        });
                    }
                }
            }

            if (String.IsNullOrEmpty(saleFileData.CpfSalesman))
            {
                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "CPF em branco.",
                    SaleFile = saleFile,
                    Line = line
                });
            }
            else if (!String.IsNullOrEmpty(saleFileData.CpfSalesman))
            {

                var user = (await _userRepository.CustomFind(x => x.Cpf.Contains(saleFileData.CpfSalesman), x => x.Office)).FirstOrDefault();

                if (user != null && user.Office.Id != (int)OfficeEnum.Salesman)
                {
                    ListSaleFileDataError.Add(new SaleFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = "Usuario não é um vendedor.",
                        SaleFile = saleFile,
                        Line = line
                    });
                }

                if (!Validation.ValidaCPF(saleFileData.CpfSalesman))
                {
                    ListSaleFileDataError.Add(new SaleFileDataError
                    {
                        CreatedAt = DateTime.Now,
                        Description = "CPF inválido.",
                        SaleFile = saleFile,
                        Line = line
                    });
                }
            }

            if (String.IsNullOrEmpty(saleFileData.NameSalesman))
            {
                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Nome em branco.",
                    SaleFile = saleFile,
                    Line = line
                });
            }
            else if (!saleFileData.NameSalesman.ToCharArray().All(t => !char.IsDigit(t)))
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Nome inválido.",
                    SaleFile = saleFile,
                    Line = line,
                });
            }

            if (saleFileData.Amount == 0)
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Quantidade em branco.",
                    SaleFile = saleFile,
                    Line = line
                });

            }

            if (saleFileData.Amount < 0)
            {

                ListSaleFileDataError.Add(new SaleFileDataError
                {
                    CreatedAt = DateTime.Now,
                    Description = "Quantidade de unidades negativa.",
                    SaleFile = saleFile,
                    Line = line
                });
            }

            return ListSaleFileDataError;
        }

        public async Task<(bool saved, IEnumerable<Sale> sales)> ProcessesSaleFile()
        {

            var saleFileDatas = await _saleFileDataRepository.CustomFind(x =>
            x.SaleFile.FileStatus.Id == (int)FileStatusEnum.SuccessfullyCompleted &&
            (x.SaleFileSkuStatus.Id == (int)FileSKUStatusEnum.AutomaticValidate ||
            x.SaleFileSkuStatus.Id == (int)FileSKUStatusEnum.Validated),
            x => x.SaleFile,
            x => x.SaleFile.Network);

            if (saleFileDatas.Any())
            {
                var saleFile = saleFileDatas.GroupBy(x => x.SaleFile).Select(x => x.Key).ToList();
                var monthsFiltered = saleFile.GroupBy(g => g.CurrentMonth).Select(g => g.Key).ToList();
                var networksFiltered = saleFile.GroupBy(g => g.Network.Id).Select(g => g.Key).ToList();
                var productFiltered = saleFileDatas.GroupBy(g => g.Product).Select(g => g.Key).ToList();
                var shopFiltered = saleFileDatas.GroupBy(g => g.Cnpj).Select(g => g.Key).ToList();
                var userFiltered = saleFileDatas.GroupBy(g => g.CpfSalesman).Select(g => g.Key).ToList();

                var sales = new List<Sale>();

                var participantProduct = await _participantProductRepository.CustomFind(x =>
                    monthsFiltered.Contains(x.CurrentMonth) &&
                    x.CurrentYear == saleFile.First().CurrentYear &&
                    productFiltered.Contains(x.Product.Id) &&
                    networksFiltered.Contains(x.Network.Id) &&
                    x.Activated);

                var focusProduct = await _focusProductRepository.CustomFind(x =>
                monthsFiltered.Contains(x.CurrentMonth) &&
                x.CurrentYear == saleFile.First().CurrentYear &&
                productFiltered.Contains(x.Product.Id) &&
                networksFiltered.Contains(x.Network.Id) &&
                x.Activated);


                var fileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.ProcessedSales);
                var products = await _productRepository.CustomFind(x => productFiltered.Contains(x.Id));
                var shops = await _shopRepository.CustomFind(x => shopFiltered.Contains(x.Cnpj));
                var users = await _userRepository.CustomFind(x => userFiltered.Contains(x.Cpf), x => x.UserStatus);

                foreach (var item in saleFileDatas)
                {

                    var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                    var shop = shops.Where(x => x.Cnpj.Equals(item.Cnpj)).FirstOrDefault();

                    var participantProductFiltered = participantProduct.Where(x =>
                    item.SaleFile.CurrentMonth == x.CurrentMonth &&
                    item.SaleFile.CurrentYear == x.CurrentYear &&
                    item.Product == x.Product.Id &&
                    item.SaleFile.Network.Id == x.Network.Id).FirstOrDefault();

                    var focusProductFiltered = focusProduct.Where(x =>
                    item.SaleFile.CurrentMonth == x.CurrentMonth &&
                    item.SaleFile.CurrentYear == x.CurrentYear &&
                    item.Product == x.Product.Id &&
                    item.SaleFile.Network.Id == x.Network.Id).FirstOrDefault();

                    var unitvalue = (focusProductFiltered != null || participantProductFiltered != null ? (focusProductFiltered != null ? focusProductFiltered.Punctuation : participantProductFiltered.Punctuation) : 0);
                    var user = users.Where(x => x.Cpf.Equals(item.CpfSalesman)).FirstOrDefault();

                    if (unitvalue > 0 && user != null && shop != null && product != null && user.UserStatus.Id != (int)UserStatusEnum.PreRegistration && user.UserStatus.Id != (int)UserStatusEnum.Inactive)
                    {
                        var sale = new Sale
                        {
                            Activated = false,
                            Processed = false,
                            Amout = item.Amount,
                            CreatedAt = DateTime.Now,
                            Network = item.SaleFile.Network,
                            Product = product,
                            SaleDate = item.SaleDate,
                            CurrentMonth = item.SaleFile.CurrentMonth,
                            CurrentYear = item.SaleFile.CurrentYear,
                            Shop = shop,
                            UnitValue = unitvalue,
                            User = user,
                        };

                        sale.TotalValue = sale.Amout * sale.UnitValue;
                        sale.Punctuation = sale.TotalValue;

                        sales.Add(sale);
                    }

                }

                if (sales.Any())
                {
                    saleFile.ForEach(sale => sale.FileStatus = fileStatus);
                    _saleRepository.SaveMany(sales);

                    return (await _unitOfWork.CommitAsync(), sales);
                }
                return (false, sales);

            }

            return (false, new List<Sale>());

        }

        public async Task ProcessesPunctuation()
        {
            var sales = await _saleRepository.CustomFind(x => x.Activated && !x.Processed, x => x.User, x => x.Shop);
            var punctuationList = new List<UserPunctuation>();

            if (sales.Any())
            {

                await ProcessesSalesman(sales);

                await ProcessesManagers(sales);

                await ProcessesRegionManagers(sales);

                await _unitOfWork.CommitAsync();

            }

        }

        private async Task ProcessesSalesman(IEnumerable<Sale> sales)
        {
            var sourceSalesman = await _userPunctuationSourceRepository.GetById((int)UserPunctuationSourceEnum.Sales);

            foreach (var sale in sales)
            {
                _userPunctuationRepository.Save(new UserPunctuation
                {
                    CreatedAt = DateTime.Now,
                    CurrentMonth = sale.CurrentMonth,
                    CurrentYear = sale.CurrentYear,
                    Description = $"PONTUAÇÃO VENDAS {sale.CurrentMonth}/{sale.CurrentYear}",
                    OperationType = 'C',
                    Punctuation = sale.Punctuation,
                    ReferenceEntityId = sale.Id,
                    User = sale.User,
                    UserPunctuationSource = sourceSalesman
                });

                sale.Processed = true;
            }
        }

        private async Task ProcessesManagers(IEnumerable<Sale> sales)
        {
            var sourceTop = await _userPunctuationSourceRepository.GetById((int)UserPunctuationSourceEnum.TopPunctuation);
            var shops = sales.GroupBy(x => x.Shop.Id).Select(x => x.Key).ToList();

            var shopsManager = await _shopUserRepository.CustomFind(x =>
            x.User.Office.Id == (int)OfficeEnum.Manager &&
            shops.Contains(x.Shop.Id) &&
            x.User.UserStatus.Id != (int)UserStatusEnum.PreRegistration &&
            x.User.UserStatus.Id != (int)UserStatusEnum.Inactive, x => x.Shop, x => x.User);

            foreach (var shop in shops)
            {
                var filteredSale = sales.Where(x => x.Shop.Id == shop);

                if (filteredSale.Any())
                {
                    var total = filteredSale.Sum(x => x.Punctuation);

                    if (total > 0)
                    {
                        var users = shopsManager.Where(x => x.Shop.Id == shop).GroupBy(x => x.User).Select(x => x.Key).ToList();

                        users.ForEach(user =>
                        {
                            _userPunctuationRepository.Save(new UserPunctuation
                            {
                                CreatedAt = DateTime.Now,
                                CurrentMonth = filteredSale.First().CurrentMonth,
                                CurrentYear = filteredSale.First().CurrentYear,
                                Description = "PONTUAÇÃO MAISTOP SUPERIOR",
                                OperationType = 'C',
                                Punctuation = total * (decimal)0.1,
                                User = user,
                                UserPunctuationSource = sourceTop
                            });
                        });
                    }
                }
            }
        }

        private async Task ProcessesRegionManagers(IEnumerable<Sale> sales)
        {
            var sourceTop = await _userPunctuationSourceRepository.GetById((int)UserPunctuationSourceEnum.TopPunctuation);
            var shops = sales.GroupBy(x => x.Shop.Id).Select(x => x.Key).ToList();

            var shopsRegionManager = await _shopUserRepository.CustomFind(x =>
            x.User.Office.Id == (int)OfficeEnum.RegionManager &&
            shops.Contains(x.Shop.Id) &&
            x.User.UserStatus.Id != (int)UserStatusEnum.PreRegistration &&
            x.User.UserStatus.Id != (int)UserStatusEnum.Inactive, x => x.Shop, x => x.User);

            foreach (var shop in shops)
            {
                var filteredSale = sales.Where(x => x.Shop.Id == shop);

                if (filteredSale.Any())
                {
                    var total = filteredSale.Sum(x => x.Punctuation);

                    if (total > 0)
                    {
                        var users = shopsRegionManager.Where(x => x.Shop.Id == shop).GroupBy(x => x.User).Select(x => x.Key).ToList();

                        users.ForEach(user =>
                        {
                            _userPunctuationRepository.Save(new UserPunctuation
                            {
                                CreatedAt = DateTime.Now,
                                CurrentMonth = filteredSale.First().CurrentMonth,
                                CurrentYear = filteredSale.First().CurrentYear,
                                Description = "PONTUAÇÃO MAISTOP SUPERIOR",
                                OperationType = 'C',
                                Punctuation = total * (decimal)0.05,
                                User = user,
                                UserPunctuationSource = sourceTop
                            });
                        });
                    }
                }
            }
        }

        public void Dispose()
        {

        }

    }
}
