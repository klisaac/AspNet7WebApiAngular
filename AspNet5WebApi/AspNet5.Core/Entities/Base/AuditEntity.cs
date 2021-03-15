using System;

namespace AspNet5.Core.Entities.Base
{
    public abstract class AuditEntity : IAuditEntity
    {
        public bool IsDeleted { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}
