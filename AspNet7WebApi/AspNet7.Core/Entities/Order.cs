using System.Collections.Generic;
using AspNet7.Core.Entities.Base;

namespace AspNet7.Core.Entities
{
    public class Order : AuditEntity
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public int BillingAddressId { get; set; }
        public virtual Address BillingAddress { get; set; }
        public int ShippingAddressId { get; set; }
        public virtual Address ShippingAddress { get; set; }
        public OrderStatus Status { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

    public enum OrderStatus
    {
        Draft = 1,
        Canceled = 2,
        Closed = 3
    }
}
