using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ITrainingService
    {
        Task ProcessesAcademyTraining();
        Task ProcessesAcademyTrainingManagers();
        Task<MyTeamTrainingDto> GetTrainingTeamSales(int shop, int currentMonth, int currentYear);
        Task<IEnumerable<TrainingManagerDto>> GetTrainingManagersReport(TrainingManagerFilterDto trainingManagerFilterDto);
    }
}
