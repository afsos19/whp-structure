using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IPhraseologyCategoryService
    {
        Task<(bool Saved, PhraseologyCategory Phraseology)> SavePhraseologyCategory(PhraseologyCategoryDto phraseologyDto);
        Task<(bool Updated, PhraseologyCategory Phraseology)> UpdatePhraseologyCategory(PhraseologyCategoryDto phraseologyDto);
        Task<IEnumerable<PhraseologyCategory>> GetAllPhraseologyCategories();
        Task<PhraseologyCategory> GetPhraseologyCategory(int id);
    }
}
