using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(int id);
        Task<UserDto> GetUserAccessByCpf(string cpf);
        Task<UserDto> GetUserAccessById(int id);
        Task<(UserDto user, bool updated, string returnMessage)> UpdateUser(UserDto userDto, IFormFile file);
        Task<MyTeamUserDto> GetTeamUsers(int shop, int currentMonth, int currentYear);
        Task<bool> UpdatePrivacityPolicy(int UserId, bool PrivacityPolicy);
        Task PasswordExpiration();
        Task<(User user, bool updated, string returnMessage)> UpdateUserExpiredPassword(int id, string password, string token);
        Task<(bool Sent, string Message)> SendAccessCodeExpiration(int userId);
        Task<IEnumerable<UserStatusLog>> GetUserStatusLogList(int userId);
        Task<object> GetUsersToAdmin(UserFilterDto userFilterDto);
        Task<byte[]> ExportUsersToExcel();
    }
}
