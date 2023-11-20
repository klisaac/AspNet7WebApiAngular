using AspNet7.Application.Models;

namespace AspNet7.Application.Responses
{
    public class CategoryResponse
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
