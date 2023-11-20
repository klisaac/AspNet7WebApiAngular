using AspNet7.Core.Entities;
using AspNet7.Core.Specifications.Base;

namespace AspNet7.Core.Specifications
{
    public class CustomerSpecification : BaseSpecification<Customer>
    {
        public CustomerSpecification(string name)
            : base(c => c.Name == name)
        {
            AddInclude(c => c.DefaultAddress);
        }

        public CustomerSpecification(int customerId)
            : base(c => c.CustomerId == customerId)
        {
            AddInclude(c => c.DefaultAddress);
        }
        public CustomerSpecification() : base(null)
        {
        }
    }
}
