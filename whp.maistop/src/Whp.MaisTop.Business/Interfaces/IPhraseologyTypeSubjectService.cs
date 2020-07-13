using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IPhraseologyTypeSubjectService
    {
        Task<(bool Saved, PhraseologyTypeSubject PhraseologyTypeSubject)> SavePhraseologyTypeSubject(PhraseologyTypeSubjectDto phraseologyTypeSubjectDto);
        Task<(bool Updated, PhraseologyTypeSubject PhraseologyTypeSubject)> UpdatePhraseologyTypeSubject(PhraseologyTypeSubjectDto phraseologyTypeSubjectDto);
        Task<IEnumerable<PhraseologyTypeSubject>> GetAllPhraseologyTypeSubjects();
        Task<IEnumerable<PhraseologyTypeSubject>> GetPhraseologyTypeSubjectsBySubjectId(int id);
        Task<PhraseologyTypeSubject> GetPhraseologyTypeSubject(int id);
    }
}
