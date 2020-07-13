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
    public class UserAccessCodeConfirmationRepository : Repository<UserAccessCodeConfirmation>, IUserAccessCodeConfirmationRepository
    {
        public UserAccessCodeConfirmationRepository(WhpDbContext context) : base(context)
        {

        }

        public async Task<UserAccessCodeConfirmation> GetAccessCode(int userId)
        {
            var ObjAccessCode = await CustomFind(x => x.User.Id == userId);

            return ObjAccessCode.FirstOrDefault();
        }
    }
}
