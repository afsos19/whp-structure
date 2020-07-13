using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface ISaleFileService
    {
        Task<object> GetFileStatus(FileStatusParamDto fileStatusParamDto, int network);
        Task<IEnumerable<string>> SendSaleFile(FileStatusParamDto fileStatusParamDto, IFormFile formFile, int network, int user);
        Task<object> GetPendingClassification(FilterDto filterDto);
        Task<string> UpdateSaleFileDataSku(SaleFileDataDto saleFileDataDtos);
        Task<bool> DoApprove(FilterDto filterDto);
        
    }
}
