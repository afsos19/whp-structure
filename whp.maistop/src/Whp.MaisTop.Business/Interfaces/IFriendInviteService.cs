using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IFriendInviteService
    {
        Task<string> GetAccessCodeInvite(int userId, bool update);
        Task<(bool Sent, string Message)> SendAccessCodeInvite(string cellphone, int userId);
        Task<(bool Saved, string Message)> DoCadUserInvited(AccessCodeUserInviteDto accessCodeUserInviteDto);
        Task<IEnumerable<object>> GetInvitedUsers(int UserId);
    }
}
