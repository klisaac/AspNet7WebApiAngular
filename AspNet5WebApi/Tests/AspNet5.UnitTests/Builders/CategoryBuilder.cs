using AspNet5.Core.Entities;

namespace AspNet5.UnitTests.Builders
{
    public class CategoryBuilder
    {

        private Category _category;
        public int CategoryId => 1;
        public string Name => "Laptop";
        public string Description => "All Laptops";
        public string ImageName => "laptop.jpg";
        public bool IsDeleted => false;
        public string CreatedBy => "admin";
        public CategoryBuilder()
        {
            _category = WithDefaultValues();
        }
        public Category Build()
        {
            return _category;
        }
        public Category WithDefaultValues()
        {
            var category = new Category() { CategoryId = CategoryId, Name = Name, ImageName = ImageName, IsDeleted = IsDeleted, CreatedBy = CreatedBy };
            return category;
        }
        public Category WithAllValues()
        {
            var category = new Category() { CategoryId = CategoryId, Name = Name, Description = Description, ImageName = ImageName, IsDeleted = IsDeleted, CreatedBy = CreatedBy };
            return category;
        }
    }
}
