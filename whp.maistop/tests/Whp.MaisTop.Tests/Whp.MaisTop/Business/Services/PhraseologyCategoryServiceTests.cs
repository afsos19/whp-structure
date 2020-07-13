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
    public class PhraseologyCategoryServiceTests
    {
        private IMapper _mapper;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IPhraseologyCategoryRepository> _mockPhraseologyCategoryRepository;

        public PhraseologyCategoryServiceTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockPhraseologyCategoryRepository = new Mock<IPhraseologyCategoryRepository>();

            var mappingProfile = new MappingProfile();

            var config = new MapperConfiguration(mappingProfile);
            _mapper = new Mapper(config);
        }

        [Fact]
        public async Task GetAllPhraseologyCategories_ReturningPhraseologyCategoryList()
        {

            _mockPhraseologyCategoryRepository.Setup(x => x.GetAll()).ReturnsAsync(new List<PhraseologyCategory>());

            var phraseologyCategoryService = new PhraseologyCategoryService(_mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyCategoryService.GetAllPhraseologyCategories();

            Assert.IsType<List<PhraseologyCategory>>(result);

        }

        [Fact]
        public async Task GetPhraseologyCategory_ReturningPhraseologyCategory()
        {
            _mockPhraseologyCategoryRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyCategory());

            var phraseologyCategoryService = new PhraseologyCategoryService(_mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyCategoryService.GetPhraseologyCategory(1);

            Assert.IsType<PhraseologyCategory>(result);
        }

        [Fact]
        public async Task SavePhraseologyCategory_ReturningTrueAndObject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            var phraseologyCategoryDto = new PhraseologyCategoryDto { Name = "test" };

            var phraseologyCategoryService = new PhraseologyCategoryService(_mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyCategoryService.SavePhraseologyCategory(phraseologyCategoryDto);

            Assert.True(result.Saved);
            Assert.IsType<PhraseologyCategory>(result.Phraseology);
            Assert.Equal("test", result.Phraseology.Name);

        }

        [Fact]
        public async Task UpdatePhraseologyCategory_ReturningTrueAndObject()
        {

            _mockUnitOfWork.Setup(x => x.CommitAsync()).ReturnsAsync(true);
            _mockPhraseologyCategoryRepository.Setup(x => x.GetById(It.IsAny<int>())).ReturnsAsync(new PhraseologyCategory { Id = 1, Name = "teste2" } );
            var phraseologyCategoryDto = new PhraseologyCategoryDto { Id = 1 , Name = "test1" };

            var phraseologyCategoryService = new PhraseologyCategoryService(_mockPhraseologyCategoryRepository.Object, _mockUnitOfWork.Object, _mapper);

            var result = await phraseologyCategoryService.UpdatePhraseologyCategory(phraseologyCategoryDto);

            Assert.True(result.Updated);
            Assert.IsType<PhraseologyCategory>(result.Phraseology);
            Assert.Equal("test1", result.Phraseology.Name);

        }

    }
}
