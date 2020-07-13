using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Whp.MaisTop.Business.Dto;
using Whp.MaisTop.Business.Interfaces;
using Whp.MaisTop.Domain.Entities;
using Whp.MaisTop.Domain.Interfaces.Repositories;
using Whp.MaisTop.Domain.Interfaces.UoW;

namespace Whp.MaisTop.Business.Services
{
    public class FocusProductService : IFocusProductService
    {

        private readonly IFocusProductRepository _focusProductRepository;
        private readonly IMapper _mapper;
        private readonly INetworkRepository _networkRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroupProductRepository _groupProductRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FocusProductService(IFocusProductRepository focusProductRepository, IMapper mapper, INetworkRepository networkRepository, IProductRepository productRepository, IGroupProductRepository groupProductRepository, IUnitOfWork unitOfWork)
        {
            _focusProductRepository = focusProductRepository;
            _mapper = mapper;
            _networkRepository = networkRepository;
            _productRepository = productRepository;
            _groupProductRepository = groupProductRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DoDeleteFocusProduct(int id)
        {
            var focusProduct = await _focusProductRepository.GetById(id);

            if (focusProduct == null)
                return false;

            _focusProductRepository.Delete(focusProduct);

           return  await _unitOfWork.CommitAsync();

        }

        public async Task<bool> DoSaveFocusProduct(FocusProductDto focusProductDto)
        {
            var focusProduct = _mapper.Map(focusProductDto, new FocusProduct());

            focusProduct.CreatedAt = DateTime.Now;
            focusProduct.Activated = true;
            focusProduct.GroupProduct = await _groupProductRepository.GetById(focusProductDto.GroupProductId);
            focusProduct.Product = await _productRepository.GetById(focusProductDto.ProductId);
            focusProduct.Network = await _networkRepository.GetById(focusProductDto.NetworkId);

            _focusProductRepository.Save(focusProduct);

            return await _unitOfWork.CommitAsync();

        }

        public async Task<bool> DoUpdateFocusProduct(FocusProductDto focusProductDto)
        {
            var focusProduct = await _focusProductRepository.GetById(focusProductDto.Id);

            if (focusProduct == null)
                return false;

            _mapper.Map(focusProductDto, focusProduct);

            focusProduct.GroupProduct = await _groupProductRepository.GetById(focusProductDto.GroupProductId);
            focusProduct.Product = await _productRepository.GetById(focusProductDto.ProductId);
            focusProduct.Network = await _networkRepository.GetById(focusProductDto.NetworkId);

            return await _unitOfWork.CommitAsync();
        }

        public async Task<FocusProduct> GetFocusProductById(int id) => (await _focusProductRepository.CustomFind(x => x.Id == id, x => x.Network, x => x.GroupProduct, x => x.Product)).FirstOrDefault();

        public async Task<IEnumerable<FocusProduct>> GetFocusProducts(int network = 0, int currentMonth = 0, int currentYear = 0, List<int> products = null) => await _focusProductRepository.GetFocusProduct(network, currentMonth, currentYear, products);
    }
}
