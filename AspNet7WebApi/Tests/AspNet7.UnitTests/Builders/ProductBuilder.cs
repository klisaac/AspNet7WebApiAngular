using System.Collections.Generic;
using AspNet7.Core.Entities;

namespace AspNet7.UnitTests.Builders
{
    public class ProductBuilder
    {
        private Product _product;
        private CategoryBuilder CategoryBuilder { get; } = new CategoryBuilder();
        public int ProductId => 1;
        public string Code => "P-PRANON001";
        public string Name => "Pranon Photo Printer Y 25";
        public string Slug => "-printer";
        public string Summary => "The black and white office solutions from Prano are the ideal office printers";
        public string Description => "The black and white office solutions from Prano are the ideal office printers";
        public string ImageFile => "pranon.jpg";
        public decimal? UnitPrice => 20000.50M;
        public int? UnitsInStock => 100;
        public double Star => 4.3;
        public int CategoryId => CategoryBuilder.CategoryId;
        public bool IsDeleted => false;
        public string CreatedBy => "admin";
        public ProductBuilder()
        {
            _product = WithDefaultValues();
        }
        public Product Build()
        {
            return _product;
        }
        public Product NewProductValues()
        {
            var product = new Product() { Code = Code, Name = Name, Summary = Summary, Description = Description, ImageFile = ImageFile, UnitPrice = UnitPrice, UnitsInStock = UnitsInStock, Star = Star, CategoryId = CategoryBuilder.CategoryId };
            return product;
        }
        public Product WithDefaultValues()
        {
            var product = new Product() { ProductId = ProductId, Code = Code, Name = Name, Summary = Summary, ImageFile = ImageFile, UnitPrice = UnitPrice, UnitsInStock = UnitsInStock, Star = Star, CategoryId = CategoryBuilder.CategoryId };
            return product;
        }
        public Product WithAllValues()
        {
            var product = new Product() { ProductId = ProductId, Code = Code, Name = Name, Summary = Summary, Description = Description, ImageFile = ImageFile, UnitPrice = UnitPrice, UnitsInStock = UnitsInStock, Star = Star, CategoryId = CategoryBuilder.CategoryId, Category = CategoryBuilder.Build() };
            return product;
        }
        public List<Product> NewProductsList(int n)
        {
            List<Product> products = new List<Product>();
            for (int i = 1; i <= n; i++)
            {
                products.Add(new Product() { Name = (Name+i), Code =  Code, Summary = Summary, Description = Description, ImageFile = ImageFile, UnitPrice = UnitPrice, UnitsInStock = UnitsInStock, Star = Star, CategoryId = CategoryBuilder.CategoryId });
            }
            return products;
        }
        public List<Product> ProductsList(int n)
        {
            List<Product> products = new List<Product>();
            for (int i = 1; i <= n; i++)
            {
                products.Add(new Product() { ProductId = ProductId, Code = Code, Name = (Name+i), Summary = Summary, Description = Description, ImageFile = ImageFile, UnitPrice = UnitPrice, UnitsInStock = UnitsInStock, Star = Star, CategoryId = CategoryBuilder.CategoryId, Category = CategoryBuilder.Build() });
            }
            return products;
        }
    }
}
