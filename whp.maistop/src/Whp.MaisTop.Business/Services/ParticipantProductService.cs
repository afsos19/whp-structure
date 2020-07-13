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
    public class ParticipantProductService : IParticipantProductService
    {

        private readonly IParticipantProductRepository _participantProductRepository;
        private readonly IMapper _mapper;
        private readonly INetworkRepository _networkRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ParticipantProductService(IParticipantProductRepository participantProductRepository, IMapper mapper, INetworkRepository networkRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _participantProductRepository = participantProductRepository;
            _mapper = mapper;
            _networkRepository = networkRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> DoDeleteParticipantProduct(int id)
        {
            var participantProduct = await _participantProductRepository.GetById(id);

            if (participantProduct == null)
                return false;

            _participantProductRepository.Delete(participantProduct);

           return  await _unitOfWork.CommitAsync();

        }

        public async Task<bool> DoSaveParticipantProduct(ParticipantProductDto participantProductDto)
        {
            var participantProduct = _mapper.Map(participantProductDto, new ParticipantProduct());

            participantProduct.CreatedAt = DateTime.Now;
            participantProduct.Activated = true;
            participantProduct.Product = await _productRepository.GetById(participantProductDto.ProductId);
            participantProduct.Network = await _networkRepository.GetById(participantProductDto.NetworkId);

            _participantProductRepository.Save(participantProduct);

            return await _unitOfWork.CommitAsync();

        }

        public async Task<bool> DoUpdateParticipantProduct(ParticipantProductDto participantProductDto)
        {
            var participantProduct = await _participantProductRepository.GetById(participantProductDto.Id);

            if (participantProduct == null)
                return false;

            _mapper.Map(participantProductDto, participantProduct);

            participantProduct.Product = await _productRepository.GetById(participantProductDto.ProductId);
            participantProduct.Network = await _networkRepository.GetById(participantProductDto.NetworkId);

            return await _unitOfWork.CommitAsync();
        }

        public async Task<ParticipantProduct> GetParticipantProductById(int id) => (await _participantProductRepository.CustomFind(x => x.Id == id, x => x.Network, x => x.Product)).FirstOrDefault();

        public async Task<IEnumerable<ParticipantProduct>> GetParticipantProducts(int network = 0, int currentMonth = 0, int currentYear = 0, List<int> products = null) => await _participantProductRepository.GetParticipantProduct(network, currentMonth, currentYear, products);
    }
}
