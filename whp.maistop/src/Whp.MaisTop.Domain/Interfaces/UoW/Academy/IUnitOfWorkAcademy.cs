using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Whp.MaisTop.Domain.Interfaces.UoW.Academy
{
    public interface IUnitOfWorkAcademy
    {
        Task<bool> CommitAsync();
    }
}
