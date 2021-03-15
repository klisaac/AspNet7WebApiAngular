using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using AspNet5.Core.Entities.Base;

namespace AspNet5.Core.Entities
{
    public class Address : AuditEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int AddressId { get; set; }
        public string AddressType { get; set; }
        public string AddressLine { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public ICollection<Customer> Customers { get; set; }
    }
}
