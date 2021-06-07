using System;
using System.Collections.Generic;

namespace AssetVariation.Domain.Entities
{
    public class AssetEntity : BaseEntity
    {       
        public string Name { get; private set; }
        public bool Active { get; private set; }

        public ICollection<TraddingFloorEntity> TraddingFloors  { get; private set; }

        public DateTime? UpdateAt { get; private set; }

        protected AssetEntity()
        {
            errors = new List<string>();
            TraddingFloors = new HashSet<TraddingFloorEntity>();
        }

        public AssetEntity(string name)
        {
            TraddingFloors = new HashSet<TraddingFloorEntity>();
            errors = new List<string>();

            Name = name;

            Validate();
        }

        public void Activate()
        {
            this.Active = true;
            this.UpdateAt = DateTime.Now;
        }

        public void Inactivate()
        {
            this.Active = false;
            this.UpdateAt = DateTime.Now;
        }

        public void AddTraddingFloor(TraddingFloorEntity traddingFloorEntity)
        {
          
            if (traddingFloorEntity.Validate())
                this.TraddingFloors.Add(traddingFloorEntity);
        }

        public override bool Validate()
        {
            if (string.IsNullOrWhiteSpace(this.Name))
                errors.Add("O nome do ativo é requerido!");

            this.Active = errors.Count == 0;

            return this.Active;
        }        

        public override string ToString()
        {
            return $"Id:{this.Id} Name: {this.Name} Active: {(this.Active ? "Y":"N")}";
        }
    }
}
