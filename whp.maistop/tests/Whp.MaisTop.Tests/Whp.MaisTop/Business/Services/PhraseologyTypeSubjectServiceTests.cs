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
    public class PhraseologyTypeSubjectServiceTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPhraseologyTypeSubjectRepository> _mockPhraseologyTypeSubjectRepository;
        private Mock<IPhraseologySubjectRepository> _mockPhraseologySubjectRepository;

        public PhraseologyTypeSubjectServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPhraseologyTypeSubjectRepository = new Mock<IPhraseologyTypeSubjectRepository>();
            _mockPhraseologySubjectRepository = new Mock<IPhraseologySubjectRepository>();

            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);
        }

        [Fact]
        public async Task GetPhraseologyTypeSubjectsBySubjectId_ReturningPhraseologyTypeSubjectList()
        {

            _mockPhraseologyTypeSubjectRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<PhraseologyTypeSubject, bool>>>())).ReturnsAsync(new List<PhraseologyTypeSubject>());

            var phraseologyTypeSubjectService = new PhraseologyTypeSubjectService(_mockPhraseologyTypeSubjectRepository.Object, _mockPhraseologySubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyTypeSubjectService.GetPhraseologyTypeSubjectsBySubjectId(1);

            Assert.IsType<List<PhraseologyTypeSubject>>(result);
        }

        [Fact]
        public async Task GetAllPhraseologyTypeSubjects_ReturningPhraseologyTypeSubjectList()
        {

            _mockPhraseologyTypeSubjectRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<PhraseologyTypeSubject,object>>>())).ReturnsAsync(new List<PhraseologyTypeSubject>());

            var phraseologyTypeSubjectService = new PhraseologyTypeSubjectService(_mockPhraseologyTypeSubjectRepository.Object, _mockPhraseologySubjectRepository.Object , _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyTypeSubjectService.GetAllPhraseologyTypeSubjects();

            Assert.IsType<List<PhraseologyTypeSubject>>(result);

        }

        [Fact]
        public async Task GetPhraseologyTypeSubject_ReturningPhraseologyTypeSubject()
        {
            _mockPhraseologyTypeSubjectRepository.Setup(x => x.CustomFind(It.IsAny<Expression<Func<PhraseologyTypeSubject,bool>>>(), It.IsAny<Expression<Func<PhraseologyTypeSubject, object>>>())).ReturnsAsync(new List<PhraseologyTypeSubject> { new PhraseologyTypeSubject() });

            var phraseologyTypeSubjectService = new PhraseologyTypeSubjectService(_mockPhraseologyTypeSubjectRepository.Object, _mockPhraseologySubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyTypeSubjectService.GetPhraseologyTypeSubject(1);

            Assert.IsType<PhraseologyTypeSubject>(result);
        }

        [Fact]
        public async Task SavePhraseologyTypeSubject_ReturningPhraseologyTypeSubject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var phraseologyTypeSubjectDto = new PhraseologyTypeSubjectDto { Description = "test PhraseologyTypeSubjectDto", PhraseologySubjectId = 1 };
            _mockPhraseologySubjectRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologySubject { Id = 1, Description = "teste PhraseologySubject" });

            var phraseologyTypeSubjectService = new PhraseologyTypeSubjectService(_mockPhraseologyTypeSubjectRepository.Object, _mockPhraseologySubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyTypeSubjectService.SavePhraseologyTypeSubject(phraseologyTypeSubjectDto);

            Assert.True(result.Saved);
            Assert.IsType<PhraseologyTypeSubject>(result.PhraseologyTypeSubject);
            Assert.Equal("test PhraseologyTypeSubjectDto", result.PhraseologyTypeSubject.Description);
            Assert.Equal("teste PhraseologySubject", result.PhraseologyTypeSubject.PhraseologySubject.Description);

        }

        [Fact]
        public async Task UpdatePhraseologyTypeSubject_ReturningTrueAndObject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockPhraseologyTypeSubjectRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyTypeSubject { Id = 1, Description = "test PhraseologyTypeSubjectDto 1", PhraseologySubject = new PhraseologySubject { Id = 1 , Description = "teste PhraseologySubject 1" }  } );
            var phraseologyTypeSubjectDto = new PhraseologyTypeSubjectDto { Description = "test PhraseologyTypeSubjectDto 2", PhraseologySubjectId = 1 };
            _mockPhraseologySubjectRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologySubject { Id = 1, Description = "teste PhraseologySubject 2" });

            var phraseologyTypeSubjectService = new PhraseologyTypeSubjectService(_mockPhraseologyTypeSubjectRepository.Object, _mockPhraseologySubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyTypeSubjectService.UpdatePhraseologyTypeSubject(phraseologyTypeSubjectDto);

            Assert.True(result.Updated);
            Assert.IsType<PhraseologyTypeSubject>(result.PhraseologyTypeSubject);
            Assert.Equal("test PhraseologyTypeSubjectDto 2", result.PhraseologyTypeSubject.Description);
            Assert.Equal("teste PhraseologySubject 2", result.PhraseologyTypeSubject.PhraseologySubject.Description);

        }

    }
}
