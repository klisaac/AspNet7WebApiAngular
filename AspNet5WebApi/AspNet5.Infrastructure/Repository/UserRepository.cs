using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspNet5.Core.Entities;
using AspNet5.Core.Repository;
using AspNet5.Core.Specifications.Base;
using AspNet5.Infrastructure.Data;
using AspNet5.Infrastructure.Repository.Base;

namespace AspNet5.Infrastructure.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(AspNet5DataContext AspNet5Context)
            : base(AspNet5Context)
        {
        }
    }
}
