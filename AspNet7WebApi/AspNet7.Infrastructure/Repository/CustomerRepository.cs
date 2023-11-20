using AspNet7.Core.Entities;
using AspNet7.Core.Repository;
using AspNet7.Infrastructure.Data;
using AspNet7.Infrastructure.Repository.Base;

namespace AspNet7.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AspNet7DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
