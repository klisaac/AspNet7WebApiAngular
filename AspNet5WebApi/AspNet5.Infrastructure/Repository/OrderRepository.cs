using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AspNet5.Core.Entities;
using AspNet5.Core.Repository;
using AspNet5.Infrastructure.Data;
using AspNet5.Infrastructure.Repository.Base;
using AspNet5.Core.Specifications;

namespace AspNet5.Infrastructure.Repository
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(AspNet5DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
