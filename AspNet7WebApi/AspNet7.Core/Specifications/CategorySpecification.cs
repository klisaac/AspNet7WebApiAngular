using AspNet7.Core.Entities;
using AspNet7.Core.Specifications.Base;

namespace AspNet7.Core.Specifications
{
    public class CategorySpecification : BaseSpecification<Category>
    {
        public CategorySpecification() : base(null)
        {
        }
        public CategorySpecification(string name)
            : base(c => c.Name == name)
        {
        }

        public CategorySpecification(int categoryId)
            : base(c => c.CategoryId == categoryId)
        {
        }
    }
}
