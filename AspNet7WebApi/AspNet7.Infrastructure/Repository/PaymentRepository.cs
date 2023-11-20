using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AspNet7.Core.Pagination;
using AspNet7.Core.Specifications;
using AspNet7.Infrastructure.Pagination;
using AspNet7.Core.Entities;
using AspNet7.Core.Repository;
using AspNet7.Infrastructure.Data;
using AspNet7.Infrastructure.Repository.Base;

namespace AspNet7.Infrastructure.Repository
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(AspNet7DataContext dbContext)
            : base(dbContext)
        {
        }
    }
}
