using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Business.Utils;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class ProductService : IProductService
    {

        private readonly IParticipantProductRepository _participantProductRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFocusProductRepository _focusProductRepository;
        private readonly ICategoryProductRepository _categoryProductRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostingEnvironment _env;

        public ProductService(IParticipantProductRepository participantProductRepository, IProducerRepository producerRepository, IProductRepository productRepository, IFocusProductRepository focusProductRepository, ICategoryProductRepository categoryProductRepository, IMapper mapper, IUnitOfWork unitOfWork, IHostingEnvironment env)
        {
            _participantProductRepository = participantProductRepository;
            _producerRepository = producerRepository;
            _productRepository = productRepository;
            _focusProductRepository = focusProductRepository;
            _categoryProductRepository = categoryProductRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _env = env;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts() => _mapper.Map(await _productRepository.CustomFind(x => 1 == 1, x => x.CategoryProduct, x => x.Producer), new List<ProductDto>());
        public async Task<IEnumerable<CategoryProductDto>> GetAllCategories() => _mapper.Map((await _categoryProductRepository.GetAll()).OrderBy(x => x.Name), new List<CategoryProductDto>());

        public async Task<IEnumerable<ProducerDto>> GetAllProducers() => _mapper.Map(await _producerRepository.GetAll(), new List<ProducerDto>());

        public async Task<IEnumerable<ParticipantProductDto>> GetParticipantProducts(int networkId) => _mapper.Map(await _participantProductRepository.GetCurrentParticipantProduct(networkId), new List<ParticipantProductDto>());

        public async Task<IEnumerable<FocusProductDto>> GetFocusProducts(int networkId) => _mapper.Map(await _focusProductRepository.GetCurrentFocusProduct(networkId), new List<FocusProductDto>());

        public async Task<IEnumerable<ProductDto>> GetProductByCategory(int idCategoryProduct) => _mapper.Map(await _productRepository.CustomFind(x => x.CategoryProduct.Id == idCategoryProduct), new List<ProductDto>());

        public async Task<IEnumerable<ProductDto>> GetProductByCategoryAndProducer(int idCategoryProduct, int idProducer) =>  
            _mapper.Map((await _productRepository.CustomFind(x =>
                x.CategoryProduct.Id == idCategoryProduct &&
                x.Producer.Id == idProducer)).OrderBy(x => x.Name), new List<ProductDto>());

        public async Task<bool> DoSaveProduct(ProductDto productDto, IFormFile file)
        {
            var product = _mapper.Map(productDto, new Product());

            product.CreatedAt = DateTime.Now;
            product.Activated = true;
            product.CategoryProduct = await _categoryProductRepository.GetById(productDto.CategoryProductId);
            product.Producer = await _producerRepository.GetById(productDto.ProducerId);

            if(file != null)
            {
               var uploadFile = await FileManipulator.Uploadfile(file, Path.Combine(_env.WebRootPath, $"Content/Products"), new List<string> { "JPG", "PNG", "JPEG" });

                if (uploadFile.uploaded)
                    product.Photo = uploadFile.fileName;
            }

            _productRepository.Save(product);

            return await _unitOfWork.CommitAsync();

        }

        public async Task<bool> DoUpdateProduct(ProductDto productDto, IFormFile file)
        {
            var product = await _productRepository.GetById(productDto.Id);

            if (product == null)
                return false;

            _mapper.Map(productDto, product);

            product.CategoryProduct = await _categoryProductRepository.GetById(productDto.CategoryProductId);
            product.Producer = await _producerRepository.GetById(productDto.ProducerId);

            if (file != null)
            {
                var uploadFile = await FileManipulator.Uploadfile(file, "Content/Products", new List<string> { "JPG","PNG","JPEG" });

                if (uploadFile.uploaded)
                    product.Photo = uploadFile.fileName;
            }

            return await _unitOfWork.CommitAsync();

        }

        public async Task<Product> GetProductById(int id) => (await _productRepository.CustomFind(x => x.Id == id, x => x.CategoryProduct, x => x.Producer)).FirstOrDefault();
    }
}
