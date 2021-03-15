using MediatR;
using AspNet5.Application.Models;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Commands
{
    public class CreateCategoryCommand : IRequest<CategoryResponse>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
    }
}
