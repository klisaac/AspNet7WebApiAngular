using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet5.Application.Commands;
using AspNet5.Application.Common.Exceptions;
using AspNet5.Core.Entities;
using AspNet5.Core.Logging;
using AspNet5.Core.Specifications;
using AspNet5.Core.Repository;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<CreateCategoryCommandHandler> _logger;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, ICurrentUser currentUser, IMapper mapper, IAspNet5Logger<CreateCategoryCommandHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await _categoryRepository.GetSingleAsync(new CategorySpecification(request.Name)) != null)
                throw new BadRequestException("Category name already exists.");
                
            var category = _mapper.Map<Category>(request);
            category.IsDeleted = false;
            category.CreatedBy = _currentUser.UserName;

            var categoryResponse = _mapper.Map<CategoryResponse>(await _categoryRepository.AddAsync(category));
            _logger.LogInformation($"Created category, {JsonSerializer.Serialize(categoryResponse)}.");

            return categoryResponse;
        }
    }
}