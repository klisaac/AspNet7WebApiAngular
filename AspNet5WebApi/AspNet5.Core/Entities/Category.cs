using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using AspNet5.Core.Entities.Base;


namespace AspNet5.Core.Entities
{
    public class Category : AuditEntity
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Key]
        public int CategoryId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        //public static Category Create(int categoryId, string name, string description = null)
        //{
        //    var category = new Category
        //    {
        //        CategoryId = categoryId,
        //        Name = name,
        //        Description = description
        //    };
        //    return category;
        //}
    }
}
