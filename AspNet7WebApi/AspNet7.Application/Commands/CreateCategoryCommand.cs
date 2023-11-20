using MediatR;
using AspNet7.Application.Models;
using AspNet7.Application.Responses;

namespace AspNet7.Application.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
    }
}
