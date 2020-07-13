using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Entities.Academy
{
    public class UserAcademy : BaseEntity
    {
        public string Login { get; set; }
        public bool Activated { get; set; }
    }
}
