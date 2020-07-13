using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class PhraseologySubjectService : IPhraseologySubjectService
    {
        private readonly IPhraseologySubjectRepository _phraseologySubjectRepository;
        private readonly IPhraseologyCategoryRepository _phraseologyCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhraseologySubjectService(IPhraseologySubjectRepository phraseologySubjectRepository, IPhraseologyCategoryRepository phraseologyCategoryRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _phraseologySubjectRepository = phraseologySubjectRepository;
            _phraseologyCategoryRepository = phraseologyCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhraseologySubject>> GetAllPhraseologySubjects() => await _phraseologySubjectRepository.GetAll(x => x.PhraseologyCategory);
        public async Task<PhraseologySubject> GetPhraseologySubject(int id) => (await _phraseologySubjectRepository.CustomFind(x => x.Id == id, x => x.PhraseologyCategory)).FirstOrDefault();
        public async Task<IEnumerable<PhraseologySubject>> GetPhraseologySubjectsByCategoryId(int id) => await _phraseologySubjectRepository.CustomFind(x => x.PhraseologyCategory.Id == id);
        public async Task<(bool Saved, PhraseologySubject PhraseologySubject)> SavePhraseologySubject(PhraseologySubjectDto PhraseologySubjectDto)
        {
            var PhraseologySubject = new PhraseologySubject();
            var PhraseologyCategory = await _phraseologyCategoryRepository.GetById(PhraseologySubjectDto.PhraseologyCategoryId);

            if (PhraseologyCategory == null)
                return (false, PhraseologySubject);

            PhraseologySubject = _mapper.Map(PhraseologySubjectDto, PhraseologySubject);
            PhraseologySubject.PhraseologyCategory = PhraseologyCategory;

            _phraseologySubjectRepository.Save(PhraseologySubject);

            return (await _unitOfWork.CommitAsync(), PhraseologySubject);
        }

        public async Task<(bool Updated, PhraseologySubject PhraseologySubject)> UpdatePhraseologySubject(PhraseologySubjectDto PhraseologySubjectDto)
        {
            var PhraseologySubject = await _phraseologySubjectRepository.GetById(PhraseologySubjectDto.Id);

            var PhraseologyCategory = await _phraseologyCategoryRepository.GetById(PhraseologySubjectDto.PhraseologyCategoryId);

            if (PhraseologyCategory == null || PhraseologySubject == null)
                return (false, PhraseologySubject);

            _mapper.Map(PhraseologySubjectDto, PhraseologySubject);
            PhraseologySubject.PhraseologyCategory = PhraseologyCategory;

            return (await _unitOfWork.CommitAsync(),PhraseologySubject);

        }
    }
}
