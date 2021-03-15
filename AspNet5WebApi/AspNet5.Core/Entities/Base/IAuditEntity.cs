using System;

namespace AspNet5.Core.Entities.Base
{
    public interface IAuditEntity
    {
        bool IsDeleted { get; set; }
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
        string LastModifiedBy { get; set; }
        DateTime? LastModifiedDate { get; set; }
    }
}
