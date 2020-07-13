using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IFocusProductService
    {
        Task<IEnumerable<FocusProduct>> GetFocusProducts(int network = 0, int currentMonth = 0, int currentYear = 0, List<int> products = null);
        Task<bool> DoSaveFocusProduct(FocusProductDto productDto);
        Task<bool> DoUpdateFocusProduct(FocusProductDto productDto);
        Task<FocusProduct> GetFocusProductById(int id);
        Task<bool> DoDeleteFocusProduct(int id);
    }
}
