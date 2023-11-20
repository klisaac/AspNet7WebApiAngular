using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AspNet7.Core.Specifications;
using AspNet7.Core.Entities;
using AspNet7.Core.Repository;
using AspNet7.Infrastructure.Data;
using AspNet7.Infrastructure.Repository.Base;

namespace AspNet7.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AspNet7DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
