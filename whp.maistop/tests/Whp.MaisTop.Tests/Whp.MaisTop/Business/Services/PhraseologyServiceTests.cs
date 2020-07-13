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
    public class PhraseologyServiceTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPhraseologyTypeSubjectRepository> _mockPhraseologyTypeSubjectRepository;
        private Mock<IPhraseologyRepository> _mockPhraseologyRepository;

        public PhraseologyServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPhraseologyTypeSubjectRepository = new Mock<IPhraseologyTypeSubjectRepository>();
            _mockPhraseologyRepository = new Mock<IPhraseologyRepository>();

            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);
        }

        [Fact]
        public async Task GetAllPhraseologies_ReturningPhraseologyList()
        {

            _mockPhraseologyRepository.Setup(x => x.GetAll(It.IsAny<Expression<Func<Phraseology,object>>>())).ReturnsAsync(new List<Phraseology>());

            var phraseologyService = new PhraseologyService(_mockPhraseologyRepository.Object ,_mockPhraseologyTypeSubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyService.GetAllPhraseologies();

            Assert.IsType<List<Phraseology>>(result);

        }

        [Fact]
        public async Task GetPhraseology_ReturningPhraseology()
        {
            _mockPhraseologyRepository.Setup(x => x.CustomFind(
                It.IsAny<Expression<Func<Phraseology, bool>>>(),
                It.IsAny<Expression<Func<Phraseology, object>>>(),
                It.IsAny<Expression<Func<Phraseology, object>>>(),
                It.IsAny<Expression<Func<Phraseology, object>>>())).ReturnsAsync(new List<Phraseology> { new Phraseology() });

            var phraseologyService = new PhraseologyService(_mockPhraseologyRepository.Object, _mockPhraseologyTypeSubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyService.GetPhraseology(1);

            Assert.IsType<Phraseology>(result);
        }

        [Fact]
        public async Task SavePhraseologyCategory_ReturningTrueAndObject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var phraseologyDto = new PhraseologyDto { Answer = "test phraseologyDto", PhraseologyTypeSubjectId = 1 };
            _mockPhraseologyTypeSubjectRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyTypeSubject { Id = 1, Description = "teste PhraseologyTypeSubject" });

            var phraseologyService = new PhraseologyService(_mockPhraseologyRepository.Object, _mockPhraseologyTypeSubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyService.SavePhraseology(phraseologyDto);

            Assert.True(result.Saved);
            Assert.IsType<Phraseology>(result.Phraseology);
            Assert.Equal("test phraseologyDto", result.Phraseology.Answer);
            Assert.Equal("teste PhraseologyTypeSubject", result.Phraseology.PhraseologyTypeSubject.Description);

        }

        [Fact]
        public async Task UpdatePhraseologyCategory_ReturningTrueAndObject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockPhraseologyRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new Phraseology { Id = 1, Answer = "test phraseologyDto 1", PhraseologyTypeSubject = new PhraseologyTypeSubject { Id = 1 , Description = "teste PhraseologyTypeSubject 1" }  } );
            var phraseologyDto = new PhraseologyDto { Answer = "test phraseologyDto 2", PhraseologyTypeSubjectId = 1 };
            _mockPhraseologyTypeSubjectRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyTypeSubject { Id = 1, Description = "teste PhraseologyTypeSubject 2" });

            var phraseologyService = new PhraseologyService(_mockPhraseologyRepository.Object, _mockPhraseologyTypeSubjectRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyService.UpdatePhraseology(phraseologyDto);

            Assert.True(result.Updated);
            Assert.IsType<Phraseology>(result.Phraseology);
            Assert.Equal("test phraseologyDto 2", result.Phraseology.Answer);
            Assert.Equal("teste PhraseologyTypeSubject 2", result.Phraseology.PhraseologyTypeSubject.Description);

        }

    }
}
