using System;
using System.Linq.Expressions;
using AspNet7.Core.Entities;
using AspNet7.Core.Specifications.Base;

namespace AspNet7.Core.Specifications
{
    public class PaymentSpecification : BaseSpecification<Payment>
    {
        public PaymentSpecification() : base(null)
        {

        }
        public PaymentSpecification(int paymentId)
            : base(p => p.PaymentId == paymentId)
        {
        }

        public PaymentSpecification(Expression<Func<Payment, bool>> criteria)
            : base(criteria)
        {
        }
    }
}
