using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public class HavanService : IHavanService
    {
        private readonly ILogger _logger;
        private readonly IUserRepository _userRepository;
        private readonly IShopUserRepository _shopUserRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IUserStatusRepository _userStatusRepository;
        private readonly IUserStatusLogRepository _userStatusLogRepository;
        private readonly IOfficeRepository _officeRepository;
        private readonly ISaleFileRepository _saleFileRepository;
        private readonly ISaleFileDataRepository _saleFileDataRepository;
        private readonly IFileStatusRepository _fileStatusRepository;
        private readonly ISaleFileSkuStatusRepository _saleFileSkuStatusRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly INetworkRepository _networkRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IProductRepository _productRepository;

        public HavanService(IProductRepository productRepository,ILogger logger, IUserRepository userRepository, IShopUserRepository shopUserRepository, IShopRepository shopRepository, IUserStatusRepository userStatusRepository, IUserStatusLogRepository userStatusLogRepository, IOfficeRepository officeRepository, ISaleFileRepository saleFileRepository, ISaleFileDataRepository saleFileDataRepository, IFileStatusRepository fileStatusRepository, ISaleFileSkuStatusRepository saleFileSkuStatusRepository, ISaleRepository saleRepository, INetworkRepository networkRepository, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _shopUserRepository = shopUserRepository;
            _shopRepository = shopRepository;
            _userStatusRepository = userStatusRepository;
            _userStatusLogRepository = userStatusLogRepository;
            _officeRepository = officeRepository;
            _saleFileRepository = saleFileRepository;
            _saleFileDataRepository = saleFileDataRepository;
            _fileStatusRepository = fileStatusRepository;
            _saleFileSkuStatusRepository = saleFileSkuStatusRepository;
            _saleRepository = saleRepository;
            _networkRepository = networkRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task UpdateHierarchy()
        {
            if (DateTime.Now.Day == 11)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://cliente.havan.com.br/ClubePontuacao/Api/");
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5cb8d9a916c33a25efdf0dd1");
                    var result = await client.GetAsync("Colaborador/Cadastro");
                    if (result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK)
                    {

                        var offices = await _officeRepository.GetAll();
                        var preOrder = await _userStatusRepository.GetById((int)UserStatusEnum.PreRegistration);
                        var json = await result.Content.ReadAsStringAsync();
                        var users = new List<User>();

                        var shops = await _shopRepository.CustomFind(x => x.Network.Id == (int)NetworkEnum.Havan);

                        var havanUserIntegratio = JsonConvert.DeserializeObject<IEnumerable<HavanUserIntegrationDto>>(json);

                        if (havanUserIntegratio.Any())
                        {
                            var cpfs = new List<string>();

                            cpfs.AddRange((havanUserIntegratio.Where(x => !string.IsNullOrEmpty(x.CpfVendedor)).GroupBy(x => x.CpfVendedor).Select(x => x.Key).ToList()));
                            cpfs.AddRange((havanUserIntegratio.Where(x => !string.IsNullOrEmpty(x.CpfGerente)).GroupBy(x => x.CpfGerente).Select(x => x.Key).ToList()));
                            cpfs.AddRange((havanUserIntegratio.Where(x => !string.IsNullOrEmpty(x.CpfGerenteRegional)).GroupBy(x => x.CpfGerenteRegional).Select(x => x.Key).ToList()));

                            await _shopUserRepository.ClearShops(cpfs);

                            users.AddRange(await _userRepository.CustomFind(x => cpfs.Contains(x.Cpf), x => x.UserStatus, x => x.Office));

                            var havanUserManangers = havanUserIntegratio
                                .Where(x => !string.IsNullOrEmpty(x.CpfGerente))
                                .GroupBy(x => new { x.CpfGerente, x.NomeGerente })
                                .Select(x => new HavanUserIntegrationDto
                                {
                                    CpfGerente = x.Key.CpfGerente,
                                    NomeGerente = x.Key.NomeGerente
                                }).ToList();

                            var havanUserRegionManangers = havanUserIntegratio
                                .Where(x => !string.IsNullOrEmpty(x.CpfGerenteRegional))
                                .GroupBy(x => new { x.CpfGerenteRegional, x.NomeGerenteRegional })
                                .Select(x => new HavanUserIntegrationDto
                                {
                                    CpfGerenteRegional = x.Key.CpfGerenteRegional,
                                    NomeGerenteRegional = x.Key.NomeGerenteRegional
                                }).ToList();


                            var regionManagersResult = await DoSaveRegionManagers(havanUserIntegratio, havanUserRegionManangers, shops, offices, preOrder, users);
                            var managersResult = await DoSaveManagers(havanUserIntegratio, havanUserManangers, shops, offices, preOrder, regionManagersResult.Users);
                            await DoSaveSalesman(havanUserIntegratio, shops, offices, preOrder, managersResult.Users);

                        }

                    }
                    else
                    {
                        _logger.Fatal($"Atualização de hierarquia havan - ocorreu uma falha ao tentar acessar api de colaboradores da havan - {result.RequestMessage}");

                    }
                }
            }

        }
        public async Task<(bool Saved, List<User> Users)> DoSaveManagers(IEnumerable<HavanUserIntegrationDto> havanUserIntegratio, IEnumerable<HavanUserIntegrationDto> havanUserManangers, IEnumerable<Shop> shops, IEnumerable<Office> offices, UserStatus preOrder, List<User> users)
        {

            foreach (var item in havanUserManangers)
            {

                if (!string.IsNullOrEmpty(item.CpfGerente))
                {
                    var newUser = users.Where(x => x.Cpf.Equals(item.CpfGerente)).FirstOrDefault();

                    if (newUser == null)
                    {
                        newUser = new User
                        {
                            Activated = false,
                            Cpf = item.CpfGerente,
                            Office = offices.Where(x => x.Id == (int)OfficeEnum.Manager).FirstOrDefault(),
                            UserStatus = preOrder,
                            PrivacyPolicy = false,
                            Name = item.NomeGerente,
                            CreatedAt = DateTime.Now
                        };
                        _userRepository.Save(newUser);
                        users.Add(newUser);
                        _logger.Info($"Atualização de hierarquia havan - novo usuario inserido no programa em pre cadastro com cpf {item.CpfGerente}");
                    }
                    else
                    {
                        _logger.Info($"Atualização de hierarquia havan - atualizado usuario no programa com cpf {item.CpfGerente}");

                    }

                    if (newUser.Office.Id != (int)OfficeEnum.Manager)
                        newUser.Office = offices.Where(x => x.Id == (int)OfficeEnum.Manager).FirstOrDefault();

                    var shopList = havanUserIntegratio.Where(x => x.CpfGerente != null && x.CpfGerente.Equals(item.CpfGerente))
                        .GroupBy(x => new { x.CpfGerente, x.CodigoFilial, x.CnpjFilial })
                        .Select(x => new HavanUserIntegrationDto
                        {
                            CnpjFilial = x.Key.CnpjFilial,
                            CodigoFilial = x.Key.CodigoFilial
                        }).ToList();

                    foreach (var shopHavan in shopList)
                    {
                        var newShopUser = shops.Where(x => x.Cnpj.Equals(shopHavan.CnpjFilial) && x.ShopCode.Equals(shopHavan.CodigoFilial)).FirstOrDefault();

                        if (newShopUser != null)
                        {
                            _shopUserRepository.Save(new ShopUser
                            {
                                User = newUser,
                                Shop = newShopUser,
                                Activated = true,
                                CreatedAt = DateTime.Now
                            });

                        }
                        else
                        {
                            _logger.Warn($"Atualização de hierarquia havan - loja com cnpj {item.CnpjFilial} e codigo {item.CodigoFilial} não cadastrada no sistema");
                        }
                    }
                }
            }

            return (await _unitOfWork.CommitAsync(), users);

        }
        public async Task<(bool Saved, List<User> Users)> DoSaveRegionManagers(IEnumerable<HavanUserIntegrationDto> havanUserIntegratio, IEnumerable<HavanUserIntegrationDto> havanUserRegionManangers, IEnumerable<Shop> shops, IEnumerable<Office> offices, UserStatus preOrder, List<User> users)
        {

            foreach (var item in havanUserRegionManangers)
            {
                if (!string.IsNullOrEmpty(item.CpfGerenteRegional))
                {
                    var newUser = users.Where(x => x.Cpf.Equals(item.CpfGerenteRegional)).FirstOrDefault();

                    if (newUser == null)
                    {
                        newUser = new User
                        {
                            Activated = false,
                            Cpf = item.CpfGerenteRegional,
                            Office = offices.Where(x => x.Id == (int)OfficeEnum.RegionManager).FirstOrDefault(),
                            UserStatus = preOrder,
                            PrivacyPolicy = false,
                            Name = item.NomeGerenteRegional,
                            CreatedAt = DateTime.Now
                        };

                        _userRepository.Save(newUser);
                        users.Add(newUser);
                        _logger.Info($"Atualização de hierarquia havan - novo usuario inserido no programa em pre cadastro com cpf {item.CpfGerenteRegional}");
                    }
                    else
                    {
                        _logger.Info($"Atualização de hierarquia havan - atualizado usuario no programa com cpf {item.CpfGerenteRegional}");
                    }

                    if (newUser.Office.Id != (int)OfficeEnum.RegionManager)
                        newUser.Office = offices.Where(x => x.Id == (int)OfficeEnum.RegionManager).FirstOrDefault();

                    var shopList = havanUserIntegratio.Where(x => x.CpfGerenteRegional != null && x.CpfGerenteRegional.Equals(item.CpfGerenteRegional))
                        .GroupBy(x => new { x.CpfGerenteRegional, x.CodigoFilial, x.CnpjFilial })
                        .Select(x => new HavanUserIntegrationDto
                        {
                            CnpjFilial = x.Key.CnpjFilial,
                            CodigoFilial = x.Key.CodigoFilial
                        }).ToList();


                    foreach (var shopHavan in shopList)
                    {
                        var newShopUser = shops.Where(x => x.Cnpj.Equals(shopHavan.CnpjFilial) && x.ShopCode.Equals(shopHavan.CodigoFilial)).FirstOrDefault();

                        if (newShopUser != null)
                        {

                            _shopUserRepository.Save(new ShopUser
                            {
                                User = newUser,
                                Shop = newShopUser,
                                Activated = true,
                                CreatedAt = DateTime.Now
                            });

                        }
                        else
                        {
                            _logger.Warn($"Atualização de hierarquia havan - loja com cnpj {item.CnpjFilial} e codigo {item.CodigoFilial} não cadastrada no sistema");
                        }
                    }
                }
            }

            return (await _unitOfWork.CommitAsync(), users);

        }
        public async Task<(bool Saved, List<User> Users)> DoSaveSalesman(IEnumerable<HavanUserIntegrationDto> havanUserIntegratio, IEnumerable<Shop> shops, IEnumerable<Office> offices, UserStatus preOrder, List<User> users)
        {

            foreach (var item in havanUserIntegratio)
            {
                if (!string.IsNullOrEmpty(item.CpfVendedor))
                {
                    var newShopUser = shops.Where(x => x.Cnpj.Equals(item.CnpjFilial) && x.ShopCode.Equals(item.CodigoFilial)).FirstOrDefault();
                    var currentUser = users.Where(x => x.Cpf.Equals(item.CpfVendedor)).FirstOrDefault();

                    if (currentUser == null)
                    {

                        if (newShopUser != null)
                        {
                            var newUser = new User
                            {
                                Activated = false,
                                Cpf = item.CpfVendedor,
                                Office = offices.Where(x => x.Id == (int)OfficeEnum.Salesman).FirstOrDefault(),
                                UserStatus = preOrder,
                                PrivacyPolicy = false,
                                Name = item.NomeVendedor,
                                CreatedAt = DateTime.Now
                            };

                            _userRepository.Save(newUser);
                            _shopUserRepository.Save(new ShopUser
                            {
                                User = newUser,
                                Shop = newShopUser,
                                Activated = true,
                                CreatedAt = DateTime.Now
                            });

                            users.Add(newUser);

                            _logger.Info($"Atualização de hierarquia havan - novo usuario inserido no programa em pre cadastro com cpf {item.CpfVendedor}");

                        }
                        else
                        {
                            _logger.Warn($"Atualização de hierarquia havan - loja com cnpj {item.CnpjFilial} e codigo {item.CodigoFilial} não cadastrada no sistema");
                        }
                    }
                    else
                    {
                        _logger.Info($"Atualização de hierarquia havan - atualizado usuario no programa com cpf {item.CpfVendedor}");

                        var lastStatus = currentUser.UserStatus;

                        if (currentUser.Office.Id != (int)OfficeEnum.Salesman)
                            currentUser.Office = offices.Where(x => x.Id == (int)OfficeEnum.Salesman).FirstOrDefault();

                        var lastSale = await _saleRepository.CustomFindLast(x => x.User.Id == currentUser.Id, x => x.Id);

                        if (currentUser.Office.Id == (int)OfficeEnum.Salesman && 
                            lastSale != null && lastSale.CreatedAt < DateTime.Now.AddMonths(-2) && 
                            currentUser.UserStatus.Id != (int)UserStatusEnum.FriendInvitation && 
                            currentUser.UserStatus.Id != (int)UserStatusEnum.PreRegistration && 
                            currentUser.UserStatus.Id != (int)UserStatusEnum.OnlyCatalog)
                        {
                            _logger.Info($" processamento da hierarquia em progresso - usuario com id {currentUser.Id} foi para somente catalogo por inatividade nas vendas");
                            currentUser.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.OnlyCatalog);
                        }
                        else if (currentUser.Office.Id == (int)OfficeEnum.Salesman && lastSale != null && lastSale.CreatedAt < DateTime.Now.AddMonths(-3) && currentUser.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog)
                        {
                            _logger.Info($" processamento da hierarquia em progresso - usuario com id {currentUser.Id} foi inativado por inatividade nas vendas");
                            currentUser.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Inactive);
                        }
                        else if (
                                currentUser.Office.Id == (int)OfficeEnum.Salesman && lastSale != null && lastSale.CreatedAt > DateTime.Now.AddMonths(-2) &&
                                (currentUser.UserStatus.Id == (int)UserStatusEnum.OnlyCatalog || currentUser.UserStatus.Id == (int)UserStatusEnum.Inactive || currentUser.UserStatus.Id == (int)UserStatusEnum.Off))
                        {
                            _logger.Info($" processamento da hierarquia em progresso - usuario com id {currentUser.Id} foi reativado");
                            currentUser.UserStatus = await _userStatusRepository.GetById((int)UserStatusEnum.Active);
                        }

                        _shopUserRepository.Save(new ShopUser
                        {
                            User = currentUser,
                            Shop = newShopUser,
                            Activated = true,
                            CreatedAt = DateTime.Now
                        });


                        if (currentUser.UserStatus.Id != lastStatus.Id)
                        {
                            _userStatusLogRepository.Save(new UserStatusLog
                            {
                                CreatedAt = DateTime.Now,
                                User = currentUser,
                                UserStatusTo = currentUser.UserStatus,
                                UserStatusFrom = lastStatus,
                                Description = "Processamento de hierarquia havan"
                            });
                        }

                    }
                }

            }

            return (await _unitOfWork.CommitAsync(), users);

        }
        public async Task<bool> DoProcessesSale()
        {

            if (DateTime.Now.Day == 10)
            {
                var saleFile = new SaleFile
                {
                    CreatedAt = DateTime.Now,
                    CurrentMonth = DateTime.Now.AddMonths(-1).Month,
                    CurrentYear = DateTime.Now.AddMonths(-1).Year,
                    FileName = "",
                    FileStatus = await _fileStatusRepository.GetById((int)FileStatusEnum.InProgress),
                    Network = await _networkRepository.GetById((int)NetworkEnum.Havan),
                    User = (await _userRepository.CustomFind(x => x.Cpf.Equals("rhuscaya"))).First()
                };

                var skuPendingClassification = await _saleFileSkuStatusRepository.GetAll();
                var products = await _productRepository.GetAll();

                _saleFileRepository.Save(saleFile);

                for (var i = 1; i <= DateTime.DaysInMonth(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month); i++)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri("https://cliente.havan.com.br/ClubePontuacao/Api/");
                        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "5cb8d9a916c33a25efdf0dd1");
                        var result = await client.PostAsync("Venda/Lotes", new StringContent(JsonConvert.SerializeObject(new { inicio = new DateTime(DateTime.Now.AddMonths(-1).Year, DateTime.Now.AddMonths(-1).Month, i) }), Encoding.UTF8, "application/json"));
                        if (result.IsSuccessStatusCode && result.StatusCode == System.Net.HttpStatusCode.OK)
                        {

                            var json = await result.Content.ReadAsStringAsync();

                            var havanSaleIntegration = JsonConvert.DeserializeObject<HavanSaleIntegrationDto>(json);

                            foreach (var item in havanSaleIntegration.Lotes.First().Itens)
                            {

                                var productId = 0;
                                var skuValidated = skuPendingClassification.Where(x => x.Id == (int)FileSKUStatusEnum.PendingClassification).First();

                                if (products.Where(x => x.Ean.Equals(item.Produto.Ean)).Any())
                                {
                                    skuValidated = skuPendingClassification.Where(x => x.Id == (int)FileSKUStatusEnum.AutomaticValidate).First();
                                    productId = products.Where(x => x.Ean.Equals(item.Produto.Ean)).First().Id;
                                }

                                _saleFileDataRepository.Save(new SaleFileData
                                {
                                    SaleFile = saleFile,
                                    SaleFileSkuStatus = skuValidated,
                                    Amount = item.Venda.Quantidade,
                                    Cnpj = item.Venda.CnpjLoja,
                                    CpfSalesman = item.Vendedor.Cpf,
                                    CreatedAt = DateTime.Now,
                                    NameSalesman = item.Vendedor.Nome,
                                    Product = productId,
                                    ProductDescription = item.Produto.Descricao,
                                    RequestNumber = item.Venda.NumeroCupomNota.ToString(),
                                    SaleDate = item.Venda.Data,
                                    Resale = "Havan"
                                });
                            }


                        }
                        else
                        {
                            _logger.Fatal($"Atualização de hierarquia havan - ocorreu uma falha ao tentar acessar api de colaboradores da havan - {result.RequestMessage}");

                        }
                    }
                }

                return await _unitOfWork.CommitAsync();
            }

            return false;

        }
    }
}
