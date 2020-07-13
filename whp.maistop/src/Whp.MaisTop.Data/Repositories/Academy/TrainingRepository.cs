using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Data.Context.Academy;
using Whp.MaisTop.Domain.Entities.Academy;
using Whp.MaisTop.Domain.Interfaces.Repositories.Academy;

namespace Whp.MaisTop.Data.Repositories.Academy
{
    public class TrainingRepository : RepositoryAcademy<Training>, ITrainingRepository
    {
        public TrainingRepository(WhpAcademyDbContext context) : base(context)
        {
        }
    }
}
