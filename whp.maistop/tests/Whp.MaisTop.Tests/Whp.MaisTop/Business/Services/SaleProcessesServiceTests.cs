using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;
using Xunit;

namespace Whp.MaisTop.Tests.Whp.MaisTop.Business.Services
{
    public class SaleProcessesServiceTests
    {
        private Mock<ISaleFileDataRepository> _mockSaleFileDataRepository;
        private Mock<ISaleFileDataErrorRepository> _mockSaleFileDataErrorRepository;
        private Mock<ISaleFileRepository> _mockSaleFileRepository;
        private Mock<IFileStatusRepository> _mockFileStatusRepository;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IUserStatusRepository> _mockUserStatusRepository;
        private Mock<IUserPunctuationRepository> _mockUserPunctuationRepository;
        private Mock<IUserPunctuationSourceRepository> _mockUserPunctuationSourceRepository;
        private Mock<IShopRepository> _mockShopRepository;
        private Mock<IShopUserRepository> _mockShopUserRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IOfficeRepository> _mockOfficeRepository;
        private Mock<ISaleRepository> _mockSaleRepository;
        private Mock<IUserStatusLogRepository> _mockUserStatusLogRepository;
        private Mock<IEmailService> _mockEmailService;
        private Mock<ILogger> _mockLogger;
        private Mock<IUserAccessCodeInviteRepository> _mockUserAccessCodeInviteRepository;
        private Mock<ICategoryProductRepository> _mockCategoryProductRepository;
        private Mock<ISaleFileSkuStatusRepository> _mockSaleFileSkuStatusRepository;
        private Mock<IParticipantProductRepository> _mockParticipantProductRepository;
        private Mock<IFocusProductRepository> _mockFocusProductRepository;
        private Mock<IProductRepository> _mockProductRepository;
        private Mock<INetworkRepository> _mockINetworkRepository;

        public SaleProcessesServiceTests()
        {
            _mockINetworkRepository = new Mock<INetworkRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockFocusProductRepository = new Mock<IFocusProductRepository>();
            _mockParticipantProductRepository = new Mock<IParticipantProductRepository>();
            _mockSaleFileSkuStatusRepository = new Mock<ISaleFileSkuStatusRepository>();
            _mockCategoryProductRepository = new Mock<ICategoryProductRepository>();
            _mockSaleFileDataErrorRepository = new Mock<ISaleFileDataErrorRepository>();
            _mockSaleFileDataRepository = new Mock<ISaleFileDataRepository>();
            _mockSaleFileRepository = new Mock<ISaleFileRepository>();
            _mockFileStatusRepository = new Mock<IFileStatusRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockUserStatusRepository = new Mock<IUserStatusRepository>();
            _mockUserPunctuationRepository = new Mock<IUserPunctuationRepository>();
            _mockUserPunctuationSourceRepository = new Mock<IUserPunctuationSourceRepository>();
            _mockShopRepository = new Mock<IShopRepository>();
            _mockShopUserRepository = new Mock<IShopUserRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockOfficeRepository = new Mock<IOfficeRepository>();
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockUserStatusLogRepository = new Mock<IUserStatusLogRepository>();
            _mockEmailService = new Mock<IEmailService>();
            _mockLogger = new Mock<ILogger>();
            _mockUserAccessCodeInviteRepository = new Mock<IUserAccessCodeInviteRepository>();

        }

        [Fact]
        public async Task ProcessesSaleFile_GeneratingSaleData_ReturningTrueAndSales()
        {

            var saleFileDataList = new List<SaleFileData>();
            var saleFile = new SaleFile { Id = 1, CurrentMonth = 1, CurrentYear = 2019, Network = new Network { Id = 1 } };
            saleFileDataList.Add(new SaleFileData
            {
                Product = 1,
                CpfSalesman = "00000000000",
                Cnpj = "12579125000105",
                SaleFile = saleFile,
                Amount = 1
            });

            _mockSaleFileDataRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<SaleFileData, bool>>>(), It.IsAny<Expression<Func<SaleFileData, object>>>(), It.IsAny<Expression<Func<SaleFileData, object>>>())).ReturnsAsync(saleFileDataList);
            _mockParticipantProductRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ParticipantProduct, bool>>>())).ReturnsAsync(new List<ParticipantProduct> {
              new ParticipantProduct
                {
                    CurrentMonth = 1,
                    CurrentYear = 2019,
                    Product = new Product { Id = 1 },
                    Network = new Network {  Id = 1},
                    Punctuation = 10
                }
            });
            _mockFocusProductRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<FocusProduct, bool>>>())).ReturnsAsync(new List<FocusProduct> {
              new FocusProduct
                {
                    CurrentMonth = 1,
                    CurrentYear = 2019,
                    Product = new Product { Id = 1 },
                    Network = new Network {  Id = 1},
                    Punctuation = 10
                }
            });
            _mockFileStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new FileStatus { Id = (int)FileStatusEnum.ProcessedSales });
            _mockProductRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Product, bool>>>())).ReturnsAsync(new List<Product> { new Product { Id = 1 , Name = "teste" } });
            _mockShopRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Shop, bool>>>())).ReturnsAsync(new List<Shop> { new Shop { Id = 1, Name = "teste", Cnpj = "12579125000105" } });
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>())).ReturnsAsync(new List<User> { new User { Id = 1, Name = "teste", Cpf = "00000000000", UserStatus = new UserStatus { Id = (int)UserStatusEnum.Active } } });
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var saleProcessesService = new SaleProcessesService(
                _mockEmailService.Object,
                _mockSaleFileDataErrorRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockSaleFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockINetworkRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockProductRepository.Object,
                _mockFocusProductRepository.Object,
                _mockParticipantProductRepository.Object,
                _mockOfficeRepository.Object,
                _mockUnitOfWork.Object,
                _mockSaleRepository.Object,
                _mockLogger.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockCategoryProductRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserPunctuationSourceRepository.Object);

            var result = await saleProcessesService.ProcessesSaleFile();

            Assert.True(result.saved);
            Assert.NotEmpty(result.sales);
            Assert.Equal(10, result.sales.First().Punctuation);

        }
    }
}
