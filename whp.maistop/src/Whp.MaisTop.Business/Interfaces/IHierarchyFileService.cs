using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IHierarchyFileService
    {
        Task<object> GetFileStatus(FileStatusParamDto fileStatusParamDto, int network);
        Task<HierarchyFileData> GetFileDataByCnpj(string cnpj);
        Task<IEnumerable<string>> SendHierarchyFile(FileStatusParamDto fileStatusParamDto, IFormFile formFile, int network, int user);
    }
}
