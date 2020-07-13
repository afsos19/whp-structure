using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class TrainingManagerDto
    {
        public string Network { get; set; }
        public string Cnpj { get; set; }
        public string Name { get; set; }
        public int TrainingCompleted { get; set; }
        public int TotalSalesman { get; set; }
        public int PorcentageCompleted { get; set; }
    }
}
