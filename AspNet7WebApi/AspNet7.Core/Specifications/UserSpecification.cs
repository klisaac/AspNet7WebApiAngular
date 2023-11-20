using AspNet7.Core.Entities;
using AspNet7.Core.Specifications.Base;

namespace AspNet7.Core.Specifications
{
    public class UserSpecification : BaseSpecification<User>
    {
        public UserSpecification() : base(null)
        {
        }
        public UserSpecification(string userName)
            : base(u => u.UserName == userName)
        {
        }

        public UserSpecification(int userId)
            : base(u => u.UserId == userId)
        {
        }

    }
}
