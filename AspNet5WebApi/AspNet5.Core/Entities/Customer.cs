using AspNet5.Core.Entities.Base;
using System.Collections.Generic;

namespace AspNet5.Core.Entities
{
    public class Customer : AuditEntity
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public int DefaultAddressId { get; set; }
        public virtual Address DefaultAddress { get; set; }
        public string Email { get; set; }
        public string CitizenId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
