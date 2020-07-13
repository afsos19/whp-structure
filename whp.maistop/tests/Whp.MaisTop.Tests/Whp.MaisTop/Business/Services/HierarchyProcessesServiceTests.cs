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
    public class HierarchyProcessesServiceTests
    {
        private Mock<IHierarchyFileDataRepository> _mockHierarchyFileDataRepository;
        private Mock<IHierarchyFileDataErrorRepository> _mockHierarchyFileDataErrorRepository;
        private Mock<IHierarchyFileRepository> _mockHierarchyFileRepository;
        private Mock<IFileStatusRepository> _mockFileStatusRepository;
        private Mock<ISaleFileRepository> _mockSaleFileRepository;
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

        public HierarchyProcessesServiceTests()
        {
            _mockSaleFileRepository = new Mock<ISaleFileRepository>();
            _mockHierarchyFileDataErrorRepository = new Mock<IHierarchyFileDataErrorRepository>();
            _mockHierarchyFileDataRepository = new Mock<IHierarchyFileDataRepository>();
            _mockHierarchyFileRepository = new Mock<IHierarchyFileRepository>();
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
        public async Task CreatePreRegistrationUser_CreatingUser_ReturningTrue()
        {
            _mockUserStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new UserStatus { Id = 1 });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Id = 1 } });
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var hierarchyFileData = new HierarchyFileData { Name = "teste", Cpf = "00000000000" };
            var office = new Office { Id = 1 };
            var shop = new Shop { Id = 1 };

            var result = await hierarchyProcesses.CreatePreRegistrationUser(hierarchyFileData,office, shop);

            Assert.True(result);

        }

        [Fact]
        public async Task ProcessesHierarchyFile_ProcessingHierarchyRead_CreatingUserReturningTrue()
        {
            _mockSaleFileRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<SaleFile, bool>>>(), It.IsAny<Expression<Func<SaleFile, object>>>())).ReturnsAsync(new List<SaleFile> { new SaleFile { Id = 10, FileStatus = new FileStatus { Id = 5 } } });
            _mockHierarchyFileRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<HierarchyFile, bool>>>(), It.IsAny<Expression<Func<HierarchyFile, object>>>(), It.IsAny<Expression<Func<HierarchyFile, object>>>())).ReturnsAsync(new List<HierarchyFile> { new HierarchyFile { Id = 10, FileStatus = new FileStatus { Id = 10 } } });
            _mockHierarchyFileDataRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<HierarchyFileData, bool>>>())).ReturnsAsync(new List<HierarchyFileData> { new HierarchyFileData { Id = 10, Office = "VENDEDOR" } });
            _mockShopRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Shop, bool>>>())).ReturnsAsync(new List<Shop> { new Shop { Id = 10 } });
            _mockOfficeRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Office, bool>>>())).ReturnsAsync(new List<Office> { new Office { Id = 10 } });
            _mockOfficeRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new Office { Id = 10 });
            _mockFileStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new FileStatus { Id = 10 });
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.ProcessesHierarchyFile();

            Assert.True(result);

        }

        [Fact]
        public async Task ProcessesHierarchyFile_ProcessingHierarchyRead_UpdatingUserReturningTrue()
        {
            _mockSaleFileRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<SaleFile, bool>>>(), It.IsAny<Expression<Func<SaleFile, object>>>())).ReturnsAsync(new List<SaleFile> { new SaleFile { Id = 10, FileStatus = new FileStatus { Id = 5 } } });
            _mockHierarchyFileRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<HierarchyFile, bool>>>(), It.IsAny<Expression<Func<HierarchyFile,object>>>(), It.IsAny<Expression<Func<HierarchyFile, object>>>())).ReturnsAsync(new List<HierarchyFile> { new HierarchyFile { Id = 10, FileStatus = new FileStatus { Id = 10 } } });
            _mockHierarchyFileDataRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<HierarchyFileData, bool>>>())).ReturnsAsync(new List<HierarchyFileData> { new HierarchyFileData { Id = 10, Office = "VENDEDOR"} });
            _mockUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<User, bool>>>(), It.IsAny<Expression<Func<User, object>>>())).ReturnsAsync(new List<User> { new User { Id = 10, UserStatus = new UserStatus { Id = (int)UserStatusEnum.Active } } });
            _mockShopRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Shop, bool>>>())).ReturnsAsync(new List<Shop> { new Shop { Id = 10 } });
            _mockOfficeRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<Office, bool>>>())).ReturnsAsync(new List<Office> { new Office { Id = 10 } });
            _mockOfficeRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync( new Office { Id = 10 } );
            _mockFileStatusRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync( new FileStatus { Id = 10 } );
            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);

            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

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

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
            _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.ProcessesHierarchyFile();

            Assert.True(result);

        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public async Task UpdateHierarchyUsers_NoPreRegistrationOffUserProcessing_ReturningUser(int userStatus)
        {
            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

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

            var user = new User { Id = 10, UserStatus = new UserStatus { Id = userStatus }, Office = new Office { Id = (int)OfficeEnum.Salesman } };
            var hierarchyFileData = new HierarchyFileData { Off = "SIM" };
            var office = new Office { Id = (int)OfficeEnum.Salesman };
            var shop = new Shop { Id = 10 };

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.UpdateHierarchyUsers(user, hierarchyFileData, office, shop);

            Assert.Equal((int)UserStatusEnum.OnlyCatalog, result.UserStatus.Id);
        }

        [Fact]
        public async Task UpdateHierarchyUsers_GettingPreRegistrationFromFriendInvite_ReturningUser()
        {
            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now.AddMonths(-4) });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

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

            var user = new User { Id = 10, UserStatus = new UserStatus { Id = (int)UserStatusEnum.FriendInvitation }, Office = new Office { Id = (int)OfficeEnum.Salesman } };
            var hierarchyFileData = new HierarchyFileData { Off = "NÃO" };
            var office = new Office { Id = (int)OfficeEnum.Salesman };
            var shop = new Shop { Id = 10 };

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.UpdateHierarchyUsers(user, hierarchyFileData, office, shop);

            Assert.Equal((int)UserStatusEnum.PreRegistration, result.UserStatus.Id);
        }

        [Fact]
        public async Task UpdateHierarchyUsers_GettingInactivatingNoSale_ReturningUser()
        {
            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now.AddMonths(-4) });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

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

            var user = new User { Id = 10, UserStatus = new UserStatus { Id = (int) UserStatusEnum.OnlyCatalog }, Office = new Office { Id = (int)OfficeEnum.Salesman } };
            var hierarchyFileData = new HierarchyFileData { Off = "NÃO" };
            var office = new Office { Id = (int)OfficeEnum.Salesman };
            var shop = new Shop { Id = 10 };

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.UpdateHierarchyUsers(user, hierarchyFileData, office, shop);

            Assert.Equal((int)UserStatusEnum.Inactive, result.UserStatus.Id);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(8)]
        [InlineData(9)]
        public async Task UpdateHierarchyUsers_GettingOnlyCatalogNoSale_ReturningUser(int userStatus)
        {
            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now.AddMonths(-2) });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

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

            var user = new User { Id = 10, UserStatus = new UserStatus { Id = userStatus }, Office = new Office { Id = (int)OfficeEnum.Salesman } };
            var hierarchyFileData = new HierarchyFileData { Off = "NÃO" };
            var office = new Office { Id = (int)OfficeEnum.Salesman };
            var shop = new Shop { Id = 10 };

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.UpdateHierarchyUsers(user, hierarchyFileData, office, shop);

            Assert.Equal((int)UserStatusEnum.OnlyCatalog, result.UserStatus.Id);
        }

        [Theory]
        [InlineData(8)]
        [InlineData(5)]
        [InlineData(4)]
        public async Task UpdateHierarchyUsers_ActivatingUserProcessing_ReturningUser(int userStatus)
        {
            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

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

            var user = new User { Id = 10, UserStatus = new UserStatus { Id = userStatus }, Office = new Office { Id = (int)OfficeEnum.Salesman } };
            var hierarchyFileData = new HierarchyFileData { Off = "NÃO" };
            var office = new Office { Id = (int)OfficeEnum.Salesman };
            var shop = new Shop { Id = 10 };

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.UpdateHierarchyUsers(user, hierarchyFileData, office, shop);

            Assert.Equal((int)UserStatusEnum.Active, result.UserStatus.Id);
        }

        [Fact]
        public async Task UpdateHierarchyUsers_PreRegistrationOffUserProcessing_ReturningUser()
        {
            _mockSaleRepository.Setup(x => x.CustomFindLast(It.IsAny<Expression<Func<Sale, bool>>>(), It.IsAny<Expression<Func<Sale, int>>>())).ReturnsAsync(new Sale { CreatedAt = DateTime.Now });
            _mockShopUserRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<ShopUser, bool>>>())).ReturnsAsync(new List<ShopUser> { new ShopUser { Shop = new Shop { Id = 10 } } });

            var arrangeStatus = new List<UserStatus>();

            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PreRegistration });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForEmail});
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Active });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Off });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.Inactive });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.WaitingForSMS });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.OnlyCatalog });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.PasswordExpired });
            arrangeStatus.Add(new UserStatus { Id = (int)UserStatusEnum.FriendInvitation });

            _mockUserStatusRepository.Setup(x => x.GetById(It.IsAny<int>()))
                .ReturnsAsync((int param) => arrangeStatus.AsQueryable().Where(x => x.Id == param).First());

            var user = new User { Id = 10, UserStatus = new UserStatus { Id = (int)UserStatusEnum.PreRegistration }, Office = new Office { Id = (int)OfficeEnum.Salesman } };
            var hierarchyFileData = new HierarchyFileData { Off = "SIM" };
            var office = new Office { Id = (int)OfficeEnum.Salesman };
            var shop = new Shop { Id = 10 };

            var hierarchyProcesses = new HierarchyProcessesService(
                _mockSaleFileRepository.Object,
                _mockEmailService.Object,
                _mockUserAccessCodeInviteRepository.Object,
                _mockUserPunctuationSourceRepository.Object,
                _mockUserPunctuationRepository.Object,
                _mockUserStatusLogRepository.Object,
                _mockLogger.Object,
                _mockHierarchyFileDataRepository.Object,
                _mockHierarchyFileDataErrorRepository.Object,
                _mockHierarchyFileRepository.Object,
                _mockFileStatusRepository.Object,
                _mockUserRepository.Object,
                _mockUserStatusRepository.Object,
                _mockShopRepository.Object,
                _mockShopUserRepository.Object,
                _mockUnitOfWork.Object,
                _mockOfficeRepository.Object,
                _mockSaleRepository.Object);

            var result = await hierarchyProcesses.UpdateHierarchyUsers(user, hierarchyFileData, office, shop);

            Assert.Equal((int)UserStatusEnum.Inactive, result.UserStatus.Id);
        }
    }
}
