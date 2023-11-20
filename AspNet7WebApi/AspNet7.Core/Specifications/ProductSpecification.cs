using System;
using System.Linq.Expressions;
using AspNet7.Core.Entities;
using AspNet7.Core.Specifications.Base;

namespace AspNet7.Core.Specifications
{
    public class ProductSpecification : BaseSpecification<Product>
    {
        public ProductSpecification() : base(null)
        {
            AddInclude(p => p.Category);
        }

        public ProductSpecification(int productId) : base(p => p.ProductId == productId)
        {
            AddInclude(p => p.Category);
        }

        public ProductSpecification(string productCode) : base(p => p.Code.Contains(productCode))
        {
            AddInclude(p => p.Category);
        }

        public ProductSpecification(string productCode, string productName) : base(null)
        {
            AddInclude(p => p.Category);
            Expression<Func<Product, bool>> productIdExpression = p => !string.IsNullOrEmpty(productCode) ? p.Code.Contains(productCode) : true;
            Expression<Func<Product, bool>> productNameExpression = p => !string.IsNullOrEmpty(productName) ? p.Name.ToLower().Contains(productName.ToLower()) : true;
            Criteria = productIdExpression.And(productNameExpression);
        }
    }
}
