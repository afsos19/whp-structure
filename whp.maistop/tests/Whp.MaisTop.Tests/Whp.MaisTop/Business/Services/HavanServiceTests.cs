using Moq;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;
using Xunit;

namespace Whp.MaisTop.Tests.Whp.MaisTop.Business.Services
{
    public class HavanServiceTests
    {
        private Mock<ILogger> _mockLogger;
        private Mock<IUserRepository> _mockUserRepository;
        private Mock<IShopUserRepository> _mockShopUserRepository;
        private Mock<IShopRepository> _mockShopRepository;
        private Mock<IUserStatusRepository> _mockUserStatusRepository;
        private Mock<IUserStatusLogRepository> _mockUserStatusLogRepository;
        private Mock<IOfficeRepository> _mockOfficeRepository;
        private Mock<ISaleRepository> _mockSaleRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<ISaleFileSkuStatusRepository> _mockSaleFileSkuStatusRepository;
        private Mock<IFileStatusRepository> _mockFileStatusRepository;
        private Mock<ISaleFileDataRepository> _mockSaleFileDataRepository;
        private Mock<ISaleFileRepository> _mockSaleFileRepository;
        private Mock<INetworkRepository> _mockNetworkRepository;
        private Mock<IProductRepository> _mockProductRepository;

        public HavanServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockLogger = new Mock<ILogger>();
            _mockUserRepository = new Mock<IUserRepository>();
            _mockShopUserRepository = new Mock<IShopUserRepository>();
            _mockShopRepository = new Mock<IShopRepository>();
            _mockUserStatusRepository = new Mock<IUserStatusRepository>();
            _mockUserStatusLogRepository = new Mock<IUserStatusLogRepository>();
            _mockOfficeRepository = new Mock<IOfficeRepository>();
            _mockSaleRepository = new Mock<ISaleRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockSaleFileRepository = new Mock<ISaleFileRepository>();
            _mockSaleFileDataRepository = new Mock<ISaleFileDataRepository>();
            _mockFileStatusRepository = new Mock<IFileStatusRepository>();
            _mockSaleFileSkuStatusRepository = new Mock<ISaleFileSkuStatusRepository>();
            _mockNetworkRepository = new Mock<INetworkRepository>();
        }

        [Fact]
        public async Task DoSaveSalesman_CreatingOrUpdatingUsers_ReturningTrueAndUpdateUserToInactivated()
        {

            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now.AddMonths(-4) });
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var arrangeStatus = new List<UserStatus>();

            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PreRegistration });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForEmail });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Active });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Off });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Inactive });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForSMS });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.OnlyCatalog });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PasswordExpired });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.FriendInvitation });

            _mockUserStatusRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync((int param) => arrangeStatus.AsQueryable().Where(x => x.Id == param).First());

            var shops = new List<Shop> { new Shop { Id = 10, Cnpj = "123123123123123123", ShopCode = "123123" } };
            var offices = new List<Office> { new Office { Id = 1 } };
            var userStatus = new UserStatus { Id = 1 };
            var havanGeneral = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CnpjFilial = "123123123123123123", CpfVendedor = "32132132132", CodigoFilial = "123123" } };
            var users = new List<User> { new User { Id = 10, Cpf = "32132132132", Office = new Office { Id = 1 }, UserStatus = new UserStatus { Id = (int)UserStatusEnum.OnlyCatalog } } };

            var service = new HavanService(
                _mockProductRepository.Object,
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockShopUserRepository.Object,
                _mockShopRepository.Object,
                _mockUserStatusRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockOfficeRepository.Object,
                _mockSaleFileRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockFileStatusRepository.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockSaleRepository.Object,
                _mockNetworkRepository.Object,
                _mockUnitOfWork.Object);

            var result = await service.DoSaveSalesman(havanGeneral, shops, offices, userStatus, users);

            Assert.Equal(1, (int)result.Users.Count);
            Assert.Equal((int)UserStatusEnum.Inactive, result.Users.First().UserStatus.Id);
            Assert.True(result.Saved);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        public async Task DoSaveSalesman_CreatingOrUpdatingUsers_ReturningTrueAndUpdateUserToOnlyCatalog(int paramUserStatus)
        {

            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now.AddMonths(-2) });
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var arrangeStatus = new List<UserStatus>();

            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PreRegistration });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForEmail });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Active });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Off });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Inactive });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForSMS });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.OnlyCatalog });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PasswordExpired });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.FriendInvitation });

            _mockUserStatusRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync((int param) => arrangeStatus.AsQueryable().Where(x => x.Id == param).First());

            var shops = new List<Shop> { new Shop { Id = 10, Cnpj = "123123123123123123", ShopCode = "123123" } };
            var offices = new List<Office> { new Office { Id = 1 } };
            var userStatus = new UserStatus { Id = 1 };
            var havanGeneral = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CnpjFilial = "123123123123123123", CpfVendedor = "32132132132", CodigoFilial = "123123" } };
            var users = new List<User> { new User { Id = 10, Cpf = "32132132132", Office = new Office { Id = 1 }, UserStatus = new UserStatus { Id = paramUserStatus } } };

            var service = new HavanService(
                _mockProductRepository.Object,
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockShopUserRepository.Object,
                _mockShopRepository.Object,
                _mockUserStatusRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockOfficeRepository.Object,
                _mockSaleFileRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockFileStatusRepository.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockSaleRepository.Object,
                _mockNetworkRepository.Object,
                _mockUnitOfWork.Object);

            var result = await service.DoSaveSalesman(havanGeneral, shops, offices, userStatus, users);

            Assert.Equal(1, (int)result.Users.Count);
            Assert.Equal((int)UserStatusEnum.OnlyCatalog, result.Users.First().UserStatus.Id);
            Assert.True(result.Saved);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(5)]
        [InlineData(4)]
        public async Task DoSaveSalesman_CreatingOrUpdatingUsers_ReturningTrueAndUpdateUserToActivated(int paramUserStatus)
        {

            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now });
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var arrangeStatus = new List<UserStatus>();

            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PreRegistration });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForEmail });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Active });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Off });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Inactive });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForSMS });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.OnlyCatalog });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PasswordExpired });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.FriendInvitation });

            _mockUserStatusRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync((int param) => arrangeStatus.AsQueryable().Where(x => x.Id == param).First());

            var shops = new List<Shop> { new Shop { Id = 10, Cnpj = "123123123123123123", ShopCode = "123123" } };
            var offices = new List<Office> { new Office { Id = 1 } };
            var userStatus = new UserStatus { Id = 1 };
            var havanGeneral = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CnpjFilial = "123123123123123123", CpfVendedor = "32132132132", CodigoFilial = "123123" } };
            var users = new List<User> { new User { Id = 10, Cpf = "32132132132", Office = new Office { Id = 1 }, UserStatus = new UserStatus { Id = paramUserStatus } } };

            var service = new HavanService(
                _mockProductRepository.Object,
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockShopUserRepository.Object,
                _mockShopRepository.Object,
                _mockUserStatusRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockOfficeRepository.Object,
                _mockSaleFileRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockFileStatusRepository.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockSaleRepository.Object,
                _mockNetworkRepository.Object,
                _mockUnitOfWork.Object);

            var result = await service.DoSaveSalesman(havanGeneral, shops, offices, userStatus, users);

            Assert.Equal(1, (int)result.Users.Count);
            Assert.Equal((int)UserStatusEnum.Active, result.Users.First().UserStatus.Id);
            Assert.True(result.Saved);
        }

        [Fact]
        public async Task DoSaveSalesman_CreatingOrUpdatingUsers_ReturningTrueAndCreatedOne()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var shops = new List<Shop> { new Shop { Id = 10, Cnpj = "123123123123123123", ShopCode = "123123" } };
            var offices = new List<Office> { new Office { Id = 1 } };
            var userStatus = new UserStatus { Id = 1 };
            var havanGeneral = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CnpjFilial = "123123123123123123", CpfVendedor = "12312312312", CodigoFilial = "123123" } };
            var users = new List<User> { new User { Id = 10, Cpf = "32132132132", Office = new Office { Id = 1 } } };

            var service = new HavanService(
                _mockProductRepository.Object,
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockShopUserRepository.Object,
                _mockShopRepository.Object,
                _mockUserStatusRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockOfficeRepository.Object,
                _mockSaleFileRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockFileStatusRepository.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockSaleRepository.Object,
                _mockNetworkRepository.Object,
                _mockUnitOfWork.Object);

            var result = await service.DoSaveSalesman(havanGeneral, shops, offices, userStatus, users);

            Assert.Equal(2, (int)result.Users.Count);
            Assert.True(result.Saved);
        }

        [Fact]
        public async Task DoSaveRegionManagers_CreatingOrUpdatingUsers_ReturningTrueAndCreatedOne()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var shops = new List<Shop> { new Shop { Id = 10, Cnpj = "123123123123123123", ShopCode = "123123" } };
            var offices = new List<Office> { new Office { Id = 3 } };
            var userStatus = new UserStatus { Id = 1 };
            var havanGeneral = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CnpjFilial = "123123123123123123", CpfGerenteRegional = "12312312312", CodigoFilial = "123123" } };
            var havanManagers = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CpfGerenteRegional = "12312312312", NomeGerenteRegional = "NomeGerente" } };
            var users = new List<User> { new User { Id = 10, Cpf = "32132132132", Office = new Office { Id = 1 } } };

            var service = new HavanService(
                _mockProductRepository.Object,
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockShopUserRepository.Object,
                _mockShopRepository.Object,
                _mockUserStatusRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockOfficeRepository.Object,
                _mockSaleFileRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockFileStatusRepository.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockSaleRepository.Object,
                _mockNetworkRepository.Object,
                _mockUnitOfWork.Object);

            var result = await service.DoSaveRegionManagers(havanGeneral, havanManagers, shops, offices, userStatus, users);

            Assert.Equal(2, (int)result.Users.Count);
            Assert.True(result.Saved);
        }

        [Fact]
        public async Task DoSaveManagers_CreatingOrUpdatingUsers_ReturningTrueAndCreatedOne()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var shops = new List<Shop> { new Shop { Id = 10, Cnpj = "123123123123123123", ShopCode = "123123" } };
            var offices = new List<Office> { new Office { Id = 2 } };
            var userStatus = new UserStatus { Id = 1 };
            var havanGeneral = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto { CnpjFilial = "123123123123123123", CpfGerente = "12312312312", CodigoFilial = "123123" } };
            var havanManagers = new List<HavanUserIntegrationDto> { new HavanUserIntegrationDto {  CpfGerente = "12312312312", NomeGerente = "NomeGerente" } };
            var users = new List<User> { new User { Id = 10, Cpf = "32132132132" , Office = new Office { Id = 1 } } };

            var service = new HavanService(
                _mockProductRepository.Object,
                _mockLogger.Object,
                _mockUserRepository.Object,
                _mockShopUserRepository.Object,
                _mockShopRepository.Object,
                _mockUserStatusRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockOfficeRepository.Object,
                _mockSaleFileRepository.Object,
                _mockSaleFileDataRepository.Object,
                _mockFileStatusRepository.Object,
                _mockSaleFileSkuStatusRepository.Object,
                _mockSaleRepository.Object,
                _mockNetworkRepository.Object,
                _mockUnitOfWork.Object);

            var result = await service.DoSaveManagers(havanGeneral, havanManagers, shops, offices, userStatus, users);

            Assert.Equal(2, (int)result.Users.Count);
            Assert.True(result.Saved);
        }
    }
}
