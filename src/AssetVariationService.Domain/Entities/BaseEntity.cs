using System;
using System.Collections.Generic;

namespace AssetVariation.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
     
        public DateTime CreateAt { get ; private set; }

        internal List<string> errors;     

        public IReadOnlyCollection<string> Errors => errors;

        public abstract bool Validate();
    }
}
