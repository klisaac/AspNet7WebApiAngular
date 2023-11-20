using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet7.Core.Specifications.Base;
using AspNet7.Core.Entities;
using AspNet7.Core.Repository;
using AspNet7.Infrastructure.Data;
using AspNet7.Infrastructure.Repository.Base;

namespace AspNet7.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AspNet7DataContext AspNet5Context)
            : base(AspNet5Context)
        {
        }
    }
}
