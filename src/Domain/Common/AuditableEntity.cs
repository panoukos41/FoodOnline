using System;

namespace FoodOnline.Domain.Common
{
    /// <summary>An entity that will have fields for when created/modified and by who.</summary>
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime Created { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime? LastModified { get; set; }
    }
}