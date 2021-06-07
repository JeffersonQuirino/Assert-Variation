using AssetVariation.Domain.Entities;
using AssetVariation.Infra.Dto;
using AutoMapper;

namespace AssetVariation.Infra.Mapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<AssetDto,AssetEntity>()
                   .ReverseMap();
            CreateMap<TraddingFloorDto,TraddingFloorEntity>()
                   .AfterMap((t, src)=>{
                       t.Asset.Id = src.Asset.Id;                            
                   })
                   .ReverseMap();
        }
    }
}
