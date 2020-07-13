using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IOfficeService
    {
        Task<IEnumerable<Office>> GetAllOffices();
    }
}
