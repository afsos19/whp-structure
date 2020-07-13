using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class OccurrenceaAdminFilterDto
    {
        public string Name { get; set; }
        public int Office { get; set; }
        public int Network { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public DateTime? RedirectedAt { get; set; }
        public DateTime? ReturnedAt { get; set; }
        public string Cpf { get; set; }
        public string Code { get; set; }
        public int TypeContact { get; set; }
        public int Iteration { get; set; }
        public bool Catalog { get; set; }
        public bool Participant { get; set; }
        public bool Critical { get; set; }
        public int Subject { get; set; }
        public int Status { get; set; }

    }
}
