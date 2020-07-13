using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Mapping;
using Whp.MaisTop.Business.Services;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;
using Xunit;

namespace Whp.MaisTop.Tests.Whp.MaisTop.Business.Services
{
    public class PhraseologySubjectServiceTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPhraseologyCategoryRepository> _mockPhraseologyCategoryRepository;
        private Mock<IPhraseologySubjectRepository> _mockPhraseologySubjectRepository;

        public PhraseologySubjectServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPhraseologyCategoryRepository = new Mock<IPhraseologyCategoryRepository>();
            _mockPhraseologySubjectRepository = new Mock<IPhraseologySubjectRepository>();

            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);
        }

        [Fact]
        public async Task GetPhraseologyTypeSubjectsByCategoryId_ReturningPhraseologyTypeSubjectList()
        {

            _mockPhraseologySubjectRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<PhraseologySubject, bool>>>())).ReturnsAsync(new List<PhraseologySubject>());

            var phraseologySubjectService = new PhraseologySubjectService(_mockPhraseologySubjectRepository.Object, _mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologySubjectService.GetPhraseologySubjectsByCategoryId(1);

            Assert.IsType<List<PhraseologySubject>>(result);
        }

        [Fact]
        public async Task GetAllPhraseologySubjects_ReturningPhraseologySubjectList()
        {

            _mockPhraseologySubjectRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<PhraseologySubject,object>>>())).ReturnsAsync(new List<PhraseologySubject>());

            var phraseologySubjectService = new PhraseologySubjectService(_mockPhraseologySubjectRepository.Object, _mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologySubjectService.GetAllPhraseologySubjects();

            Assert.IsType<List<PhraseologySubject>>(result);

        }

        [Fact]
        public async Task GetPhraseologySubject_ReturningPhraseologySubject()
        {
            _mockPhraseologySubjectRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<PhraseologySubject, bool>>>(), It.IsAny<Expression<Func<PhraseologySubject, object>>>())).ReturnsAsync(new List<PhraseologySubject> { new PhraseologySubject() });

            var phraseologySubjectService = new PhraseologySubjectService(_mockPhraseologySubjectRepository.Object, _mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologySubjectService.GetPhraseologySubject(1);

            Assert.IsType<PhraseologySubject>(result);
        }

        [Fact]
        public async Task SavePhraseologySubject_ReturningPhraseologySubject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var phraseologySubjectDto = new PhraseologySubjectDto { Description = "test PhraseologySubjectDto", PhraseologyCategoryId = 1 };
            _mockPhraseologyCategoryRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyCategory { Id = 1, Name = "teste PhraseologyCategory" });

            var phraseologySubjectService = new PhraseologySubjectService(_mockPhraseologySubjectRepository.Object, _mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologySubjectService.SavePhraseologySubject(phraseologySubjectDto);

            Assert.True(result.Saved);
            Assert.IsType<PhraseologySubject>(result.PhraseologySubject);
            Assert.Equal("test PhraseologySubjectDto", result.PhraseologySubject.Description);
            Assert.Equal("teste PhraseologyCategory", result.PhraseologySubject.PhraseologyCategory.Name);

        }

        [Fact]
        public async Task UpdatePhraseologySubject_ReturningTrueAndObject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockPhraseologySubjectRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologySubject { Id = 1, Description = "test PhraseologySubjectDto 1", PhraseologyCategory = new PhraseologyCategory { Id = 1 , Name = "teste PhraseologyCategory 1" }  } );
            var phraseologySubjectDto = new PhraseologySubjectDto { Description = "test PhraseologySubjectDto 2", PhraseologyCategoryId = 1 };
            _mockPhraseologyCategoryRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyCategory { Id = 1, Name = "teste PhraseologyCategory 2" });

            var phraseologySubjectService = new PhraseologySubjectService(_mockPhraseologySubjectRepository.Object, _mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologySubjectService.UpdatePhraseologySubject(phraseologySubjectDto);

            Assert.True(result.Updated);
            Assert.IsType<PhraseologySubject>(result.PhraseologySubject);
            Assert.Equal("test PhraseologySubjectDto 2", result.PhraseologySubject.Description);
            Assert.Equal("teste PhraseologyCategory 2", result.PhraseologySubject.PhraseologyCategory.Name);

        }

    }
}
