using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNet5.Core.Entities;
using AspNet5.Core.Pagination;
using AspNet5.Core.Repository;
using AspNet5.Core.Specifications;
using AspNet5.Infrastructure.Data;
using AspNet5.Infrastructure.Pagination;
using AspNet5.Infrastructure.Repository.Base;

namespace AspNet5.Infrastructure.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AspNet5DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
