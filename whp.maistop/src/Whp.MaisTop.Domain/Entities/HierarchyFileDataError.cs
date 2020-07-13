using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities
{
    public class HierarchyFileDataError : BaseEntity
    {
        public HierarchyFile HierarchyFile { get; set; }
        public int Line { get; set; }
        public string Description { get; set; }
        public string Spreedsheet { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
