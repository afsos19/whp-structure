﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ISaleProcessesService : IDisposable
    {
        Task ValidateImportedStructure(string pathFrom);
        Task<(bool saved, IEnumerable<Sale> sales)> ProcessesSaleFile();
        Task ProcessesPunctuation();
    }
}
