﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class OccurrenceMessageTypeDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
