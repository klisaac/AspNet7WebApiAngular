﻿using AspNet7.Core.Entities;
using AspNet7.Core.Specifications.Base;

namespace AspNet7.Core.Specifications
{
    public class OrderSpecification : BaseSpecification<Order>
    {
        public OrderSpecification(int customerId)
            : base(o => o.CustomerId == customerId)
        {
            AddInclude(o => o.Customer);
            AddInclude("OrderItems.Product");
        }

        public OrderSpecification() : base(null)
        {
            AddInclude(o => o.Customer);
            AddInclude("OrderItems.Product");
        }
    }
}
