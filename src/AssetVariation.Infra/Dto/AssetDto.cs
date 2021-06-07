using System;
using System.Collections.Generic;

namespace AssetVariation.Infra.Dto
{
    public  class AssetDto:BaseDto
    {
        public AssetDto()
        {
            TraddingFloors = new HashSet<TraddingFloorDto>();
        }

        public string Name { get;  set; }
        public bool Active { get;  set; }
        public ICollection<TraddingFloorDto> TraddingFloors { get;  set; }
        public DateTime? UpdateAt { get;  set; }

        public static implicit operator bool(AssetDto v)
        {
            throw new NotImplementedException();
        }
    }
}
