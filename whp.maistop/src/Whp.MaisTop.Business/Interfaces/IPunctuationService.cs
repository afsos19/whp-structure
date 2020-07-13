using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IPunctuationService
    {
        Task<ExtractDto> GetUserExtract(int userId);
        Task<IEnumerable<UserPunctuation>> GetUserCredits(int userId, int currentMonth, int currentYear);
        Task<decimal> PointsToExpire(int userId);
        Task PointsExpiration();
        Task BirthdayProcessing();
    }
}
