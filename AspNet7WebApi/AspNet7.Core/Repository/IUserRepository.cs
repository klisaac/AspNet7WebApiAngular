using System.Threading.Tasks;
using AspNet7.Core.Specifications.Base;
using AspNet7.Core.Entities;
using AspNet7.Core.Repository.Base;

namespace AspNet7.Core.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
