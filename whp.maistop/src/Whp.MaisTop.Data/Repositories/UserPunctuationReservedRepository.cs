﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Enums;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class UserPunctuationReservedRepository : Repository<UserPunctuationReserved>, IUserPunctuationReservedRepository
    {
        public UserPunctuationReservedRepository(WhpDbContext context) : base(context)
        {
        }

    }
}
