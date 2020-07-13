using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IPhraseologyService
    {
        Task<(bool Saved, Phraseology Phraseology)> SavePhraseology(PhraseologyDto phraseologyDto);
        Task<(bool Updated, Phraseology Phraseology)> UpdatePhraseology(PhraseologyDto phraseologyDto);
        Task<IEnumerable<Phraseology>> GetAllPhraseologies();
        Task<Phraseology> GetPhraseology(int id);
    }
}
