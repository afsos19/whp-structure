using System;
using System.Collections.Generic;
using System.Text;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Dto
{
    public class MyTeamUserDto
    {
        public MyTeamUserDto()
        {
            Activated = 0;
            PreRegistered = 0;
            Inactivated = 0;
            UserList = new List<User>();
        }

        public int Activated { get; set; }
        public int PreRegistered { get; set; }
        public int Inactivated { get; set; }
        public IEnumerable<User> UserList { get; set; }
    }
}
