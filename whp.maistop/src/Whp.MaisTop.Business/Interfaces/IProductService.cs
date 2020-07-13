using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Domain.Entities;

namespace Whp.MaisTop.Business.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ParticipantProductDto>> GetParticipantProducts(int networkId);
        Task<IEnumerable<CategoryProductDto>> GetAllCategories();
        Task<IEnumerable<ProducerDto>> GetAllProducers();
        Task<IEnumerable<ProductDto>> GetProductByCategory(int idCategoryProduct);
        Task<IEnumerable<ProductDto>> GetProductByCategoryAndProducer(int idCategoryProduct, int idProducer);
        Task<IEnumerable<FocusProductDto>> GetFocusProducts(int networkId);
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<bool> DoSaveProduct(ProductDto productDto, IFormFile file);
        Task<bool> DoUpdateProduct(ProductDto productDto, IFormFile file);
        Task<Product> GetProductById(int id); 
    }
}
