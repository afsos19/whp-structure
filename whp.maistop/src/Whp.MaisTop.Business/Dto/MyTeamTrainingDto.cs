using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class MyTeamTrainingDto
    {
        public MyTeamTrainingDto()
        {
            TrainingOneDone = 0;
            TrainingTwoDone = 0;
            TrainingNotDone = 0;
            TrainingList = new List<object>();
        }

        public int TrainingOneDone { get; set; }
        public int TrainingTwoDone { get; set; }
        public int TrainingNotDone { get; set; }
        public IEnumerable<object> TrainingList { get; set; }
    }
}
