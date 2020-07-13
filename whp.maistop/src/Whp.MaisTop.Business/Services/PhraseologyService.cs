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
    public class PhraseologyService : IPhraseologyService
    {
        private readonly IPhraseologyRepository _phraseologyRepository;
        private readonly IPhraseologyTypeSubjectRepository _phraseologyTypeSubjectRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhraseologyService(IPhraseologyRepository phraseologyRepository, IPhraseologyTypeSubjectRepository phraseologyTypeSubjectRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _phraseologyRepository = phraseologyRepository;
            _phraseologyTypeSubjectRepository = phraseologyTypeSubjectRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Phraseology>> GetAllPhraseologies() => await _phraseologyRepository.GetAll(x => x.PhraseologyTypeSubject);

        public async Task<Phraseology> GetPhraseology(int id) => (await _phraseologyRepository.CustomFind(x => x.Id == id, x => x.PhraseologyTypeSubject, x => x.PhraseologyTypeSubject.PhraseologySubject, x => x.PhraseologyTypeSubject.PhraseologySubject.PhraseologyCategory)).FirstOrDefault();

        public async Task<(bool Saved, Phraseology Phraseology)> SavePhraseology(PhraseologyDto phraseologyDto)
        {
            var phraseology = new Phraseology();
            var phraseologyTypeSubject = await _phraseologyTypeSubjectRepository.GetById(phraseologyDto.PhraseologyTypeSubjectId);

            if (phraseologyTypeSubject == null)
                return (false, phraseology);

            phraseology = _mapper.Map(phraseologyDto, phraseology);
            phraseology.PhraseologyTypeSubject = phraseologyTypeSubject;

            _phraseologyRepository.Save(phraseology);

            return (await _unitOfWork.CommitAsync(), phraseology);
        }

        public async Task<(bool Updated, Phraseology Phraseology)> UpdatePhraseology(PhraseologyDto phraseologyDto)
        {
            var phraseology = await _phraseologyRepository.GetById(phraseologyDto.Id);

            var phraseologyTypeSubject = await _phraseologyTypeSubjectRepository.GetById(phraseologyDto.PhraseologyTypeSubjectId);

            if (phraseologyTypeSubject == null || phraseology == null)
                return (false, phraseology);

            _mapper.Map(phraseologyDto, phraseology);
            phraseology.PhraseologyTypeSubject = phraseologyTypeSubject;

            return (await _unitOfWork.CommitAsync(),phraseology);

        }
    }
}
