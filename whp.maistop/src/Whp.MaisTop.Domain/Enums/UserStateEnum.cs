using System;
using System.Collections.Generic;
using System.Text;

namespace Whp.MaisTop.Domain.Enums
{
    public enum UserStatusEnum
    {
        PreRegistration = 1,
        WaitingForEmail = 2,
        Active = 3,
        Off = 4,
        Inactive = 5,
        WaitingForSMS = 6,
        OnlyCatalog = 8,
        PasswordExpired = 9,
        FriendInvitation = 10
    }
}
