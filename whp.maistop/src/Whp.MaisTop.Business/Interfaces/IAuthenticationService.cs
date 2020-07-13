using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IAuthenticationService
    {
        Task<(UserDto user, bool authenticated, string messageReturning, string link)> DoLogin(LoginDto loginDto, string ip);
        Task<(string code, string message, bool sent)> SendSMSAccessCodeConfirmation(UserDto user);
        Task<(UserDto user, bool authenticated, string messageReturning)> DoPreRegistration(UserDto userDto, IFormFile file);
        Task<(bool sent, string message)> ForgotPassword(string cpf);
        Task<(bool found, string message, UserDto user)> FirstAccess(string cpf);
        Task<string> GetShopAuthenticate(int userId, int network);
        Task<string> GetTrainingAuthenticate(int userId, int network, string ip);
        Task DoSaveUserAccessLog(string cpf, string description, string ip, string device);

    }
}
