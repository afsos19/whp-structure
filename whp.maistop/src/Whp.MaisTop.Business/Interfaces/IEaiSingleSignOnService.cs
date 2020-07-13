using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IEaiSingleSignOnService
    {
        Task<string> CalculateSignature(string stringToSign, string key);
        Task<string> Authenticate(User user);
    }
}
