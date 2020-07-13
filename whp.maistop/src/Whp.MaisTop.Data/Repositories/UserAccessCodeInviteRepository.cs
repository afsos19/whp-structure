using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Data.Context;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;

namespace Whp.MaisTop.Data.Repositories
{
    public class UserAccessCodeInviteRepository : Repository<UserAccessCodeInvite>, IUserAccessCodeInviteRepository
    {
        public UserAccessCodeInviteRepository(WhpDbContext context) : base(context)
        {

        }

        public async Task<UserAccessCodeInvite> GetAccessCode(int userId)
        {
            var ObjAccessCode = await CustomFind(x => x.User.Id == userId);

            return ObjAccessCode.FirstOrDefault();
        }
    }
}
