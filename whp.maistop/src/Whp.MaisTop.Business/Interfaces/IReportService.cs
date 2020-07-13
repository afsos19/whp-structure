using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IReportService
    {
        Task<(bool hasReport, object report, string message)> PreProcessingSales(FilterDto filterDto);
        Task<(bool hasReport, object report, string message)> PreProcessingSalesPunctuation(FilterDto filterDto);
        Task<(bool hasReport, object report, string message)> GetSaleFile(FilterBaseTrackingDto filterDto);
        Task<(bool hasReport, object report, string message)> GetHierarchyFile(FilterBaseTrackingDto filterDto);
    }
}
