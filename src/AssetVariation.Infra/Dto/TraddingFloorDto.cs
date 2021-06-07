using System;
using System.Text.Json.Serialization;

namespace AssetVariation.Infra.Dto
{
    public class TraddingFloorDto:BaseDto
    {
        public TraddingFloorDto()
        {
            Asset = new();
        }

        public long AssetId { get;  set; }
        [JsonIgnore]
        public AssetDto Asset { get;  set; }
        public decimal Value { get;  set; }
        public decimal Variation { get;  set; }
        public DateTime OperationDate { get;  set; }
    }
}
