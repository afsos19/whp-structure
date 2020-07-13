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
    public class PhraseologyTypeSubjectService : IPhraseologyTypeSubjectService
    {
        private readonly IPhraseologyTypeSubjectRepository _phraseologyTypeSubjectRepository;
        private readonly IPhraseologySubjectRepository _phraseologySubjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhraseologyTypeSubjectService(IPhraseologyTypeSubjectRepository phraseologyTypeSubjectRepository, IPhraseologySubjectRepository phraseologySubjectRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _phraseologyTypeSubjectRepository = phraseologyTypeSubjectRepository;
            _phraseologySubjectRepository = phraseologySubjectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhraseologyTypeSubject>> GetAllPhraseologyTypeSubjects() => await _phraseologyTypeSubjectRepository.GetAll(x => x.PhraseologySubject);

        public async Task<PhraseologyTypeSubject> GetPhraseologyTypeSubject(int id) => (await _phraseologyTypeSubjectRepository.CustomFind(x => x.Id == id, x => x.PhraseologySubject)).FirstOrDefault();

        public async Task<IEnumerable<PhraseologyTypeSubject>> GetPhraseologyTypeSubjectsBySubjectId(int id) => await _phraseologyTypeSubjectRepository.CustomFind(x => x.PhraseologySubject.Id == id);
        
        public async Task<(bool Saved, PhraseologyTypeSubject PhraseologyTypeSubject)> SavePhraseologyTypeSubject(PhraseologyTypeSubjectDto PhraseologyTypeSubjectDto)
        {
            var PhraseologyTypeSubject = new PhraseologyTypeSubject();
            var PhraseologySubject = await _phraseologySubjectRepository.GetById(PhraseologyTypeSubjectDto.PhraseologySubjectId);

            if (PhraseologySubject == null)
                return (false, PhraseologyTypeSubject);

            PhraseologyTypeSubject = _mapper.Map(PhraseologyTypeSubjectDto, PhraseologyTypeSubject);
            PhraseologyTypeSubject.PhraseologySubject = PhraseologySubject;

            _phraseologyTypeSubjectRepository.Save(PhraseologyTypeSubject);

            return (await _unitOfWork.CommitAsync(), PhraseologyTypeSubject);
        }

        public async Task<(bool Updated, PhraseologyTypeSubject PhraseologyTypeSubject)> UpdatePhraseologyTypeSubject(PhraseologyTypeSubjectDto PhraseologyTypeSubjectDto)
        {
            var PhraseologyTypeSubject = await _phraseologyTypeSubjectRepository.GetById(PhraseologyTypeSubjectDto.Id);

            var PhraseologySubject = await _phraseologySubjectRepository.GetById(PhraseologyTypeSubjectDto.PhraseologySubjectId);

            if (PhraseologyTypeSubject == null || PhraseologySubject == null)
                return (false, PhraseologyTypeSubject);

            _mapper.Map(PhraseologyTypeSubjectDto, PhraseologyTypeSubject);
            PhraseologyTypeSubject.PhraseologySubject = PhraseologySubject;

            return (await _unitOfWork.CommitAsync(), PhraseologyTypeSubject);

        }
    }
}
