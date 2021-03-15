using AspNet5.Core.Entities;
using AspNet5.Core.Specifications.Base;

namespace AspNet5.Core.Specifications
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
