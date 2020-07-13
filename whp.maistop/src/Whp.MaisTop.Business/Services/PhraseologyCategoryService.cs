using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class PhraseologyCategoryService : IPhraseologyCategoryService
    {
        private readonly IPhraseologyCategoryRepository _phraseologyCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhraseologyCategoryService(IPhraseologyCategoryRepository phraseologyCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _phraseologyCategoryRepository = phraseologyCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhraseologyCategory>> GetAllPhraseologyCategories() => await _phraseologyCategoryRepository.GetAll();

        public async Task<PhraseologyCategory> GetPhraseologyCategory(int id) => await _phraseologyCategoryRepository.GetById(id);

        public async Task<(bool Saved, PhraseologyCategory Phraseology)> SavePhraseologyCategory(PhraseologyCategoryDto phraseologyDto)
        {

            var phraseology = _mapper.Map<PhraseologyCategory>(phraseologyDto);

            _phraseologyCategoryRepository.Save(phraseology);

            return (await _unitOfWork.CommitAsync(), phraseology);
        }

        public async Task<(bool Updated, PhraseologyCategory Phraseology)> UpdatePhraseologyCategory(PhraseologyCategoryDto phraseologyDto)
        {
            var phraseology = await _phraseologyCategoryRepository.GetById(phraseologyDto.Id);

            if (phraseology == null)
                return (false, phraseology);

            _mapper.Map(phraseologyDto, phraseology);

            return (await _unitOfWork.CommitAsync(),phraseology);

        }
    }
}
