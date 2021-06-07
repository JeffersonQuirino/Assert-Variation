using AssetVariation.Domain.Core.Interfaces.Repository;
using AssetVariation.Domain.Entities;
using AssetVariation.Infra.Dto;
using AssetVariation.Service.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AssetVariation.Service.Services
{
    public class TraddingFloorService : BaseService<TraddingFloorDto, TraddingFloorEntity>, ITraddingFloorService
    {
        private readonly IMapper mapper;
        private readonly ITraddingFloorRepository traddingFloorRespository;
        public TraddingFloorService(IMapper mapper,
                                    ITraddingFloorRepository traddingFloorRespository):base(mapper,traddingFloorRespository)
        {
            this.mapper = mapper;
            this.traddingFloorRespository = traddingFloorRespository;

        }       
    }
}
