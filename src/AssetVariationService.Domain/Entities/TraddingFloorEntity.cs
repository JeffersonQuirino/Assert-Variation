using System;
using System.Collections.Generic;

namespace AssetVariation.Domain.Entities
{
    public class TraddingFloorEntity : BaseEntity
    {       
        public AssetEntity Asset { get; private set; }
        public decimal Value { get; private set; }

        public decimal Variation { get; private set; }

        public DateTime OperationDate { get; private set; }

        public long AssetId { get; private set; }

        public TraddingFloorEntity( AssetEntity asset, decimal value, decimal variation, DateTime operationDate, long assetId)
        {
            errors = new List<string>();

            Asset = asset;           
            Value = Math.Round(value,4);
            Variation = Math.Round(variation,2);
            OperationDate = operationDate;
            AssetId = assetId;

            if (Asset != null)
                Asset.Id = AssetId;

            Validate();
        }

        public void SetAsset(AssetEntity asset)
        {
            this.Asset = asset;
        }

        protected TraddingFloorEntity()
        {          
            errors = new List<string>();
        }

        public override bool Validate()
        {
            if (OperationDate == DateTime.MinValue)
                this.errors.Add("A data da operação é requerida!");           

            if (Asset == null || Asset.Id == 0)
                this.errors.Add("O Ativo é requerido!");

            if (Value == 0)
                this.errors.Add("O valor do ativo deve ser maior que zero!");

            return this.errors.Count == 0;
        }
    }
}
