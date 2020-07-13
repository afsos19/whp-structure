using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IParticipantProductService
    {
        Task<IEnumerable<ParticipantProduct>> GetParticipantProducts(int network = 0, int currentMonth = 0, int currentYear = 0, List<int> products = null);
        Task<bool> DoSaveParticipantProduct(ParticipantProductDto participantProductDto);
        Task<bool> DoUpdateParticipantProduct(ParticipantProductDto participantProductDto);
        Task<ParticipantProduct> GetParticipantProductById(int id);
        Task<bool> DoDeleteParticipantProduct(int id);
    }
}
