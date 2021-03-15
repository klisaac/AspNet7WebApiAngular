using System.Collections.Generic;
using AspNet5.Core.Entities.Base;

namespace AspNet5.Core.Entities
{
    public class Payment : AuditEntity
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public virtual Order Order { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod Method { get; set; }
    }
    public enum PaymentMethod
    {
        Cash = 1,
        CreditCard = 2,
        Check = 3,
        BankTransfer = 4,
        Paypal = 5,
        Payoneer = 6
    }

}
