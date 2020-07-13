using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Business.Dto
{
    public class AdministratorUserDto 
    {
        public int Id { get; set; }
        public AdministratorUserTypeDto AdministratorUserType { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Activated { get; set; }
    }
}
