using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Domain.Entities;
using AssetVariation.Infra.Dto;
using AssetVariation.Service.Interfaces;
using AutoMapper;

namespace AssetVariation.Service.Services
{
    public class AssetService :BaseService<AssetDto,AssetEntity>, IAssetService
    {
        private readonly IMapper mapper;
        private readonly IAssetRepository assetRepository;
      
        public AssetService(IMapper mapper,
                            IAssetRepository assetRepository):base(mapper,assetRepository)
        {

            this.mapper = mapper;
            this.assetRepository = assetRepository;
        }      
    }
}