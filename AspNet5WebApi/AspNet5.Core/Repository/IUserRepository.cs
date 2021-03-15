using System.Threading.Tasks;
using AspNet5.Core.Entities;
using AspNet5.Core.Repository.Base;
using AspNet5.Core.Specifications.Base;

namespace AspNet5.Core.Repository
{
    public interface IUserRepository : IBaseRepository<User>
    {
    }
}
