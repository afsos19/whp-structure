using Whp.MaisTop.Business.Dto;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ILogsPunctuationService
    {
        LogsPunctuationDto GetLastHourLogsPunctuation();
        LogsPunctuationDto GetDailyLogsPunctuation();
    }
}
