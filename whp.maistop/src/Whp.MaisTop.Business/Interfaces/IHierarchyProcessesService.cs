using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IHierarchyProcessesService : IDisposable
    {
        Task ValidateImportedStructure(string pathFrom);
        Task<bool> ProcessesHierarchyFile();
        Task<bool> CreatePreRegistrationUser(HierarchyFileData hierarchyFileData, Office office, Shop shop);
        Task<User> UpdateHierarchyUsers(User user, HierarchyFileData hierarchyFileData, Office office, Shop shop);
        Task<IEnumerable<HierarchyFileDataError>> ValidateSpreadsheetRow(HierarchyFileData hierarchyFileData, HierarchyFile hierarchyFile, int line, int tab, string spreadsheet);
        Task<(IEnumerable<HierarchyFileDataError> errorList, IEnumerable<HierarchyFileData> dataList)> ValidateManagerTab(HierarchyFile hierarchyFile, ExcelWorksheet managerTab);
        Task<(IEnumerable<HierarchyFileDataError> errorList, IEnumerable<HierarchyFileData> dataList)> ValidateSalesmanTab(HierarchyFile hierarchyFile, ExcelWorksheet salesmanTab);
    }
}
