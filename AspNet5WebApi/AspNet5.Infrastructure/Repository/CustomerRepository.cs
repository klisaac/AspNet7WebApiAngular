using AspNet5.Core.Entities;
using AspNet5.Core.Repository;
using AspNet5.Infrastructure.Data;
using AspNet5.Infrastructure.Repository.Base;

namespace AspNet5.Infrastructure.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(AspNet5DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
