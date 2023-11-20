using AspNet7.Core.Entities.Base;

namespace AspNet7.Core.Entities
{
    public class OrderItem : AuditEntity
    {
        public int OrderItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}
