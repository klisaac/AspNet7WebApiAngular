using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet5.Application.Commands;
using AspNet5.Application.Common.Exceptions;
using AspNet5.Core.Entities;
using AspNet5.Core.Logging;
using AspNet5.Core.Repository;
using AspNet5.Core.Specifications;
using AspNet5.Application.Common.Helpers;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, ICurrentUser currentUser, IMapper mapper, IAspNet5Logger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (await _userRepository.GetSingleAsync(new UserSpecification(request.UserName)) != null)
                throw new BadRequestException($"User, {request.UserName} already exists.");
            var user = new User();
            var passwordSaltAndHash = Password.CreatePasswordHash(request.Password);

            // update user properties
            user.UserName = request.UserName;
            user.PasswordSalt = passwordSaltAndHash.Item1;
            user.PasswordHash = passwordSaltAndHash.Item2;
            user.IsDeleted = false;
            // _currentUserService.UserName is null as API action to create user is anonymous.
            //user.CreatedBy = _currentUserService.UserName;
            user.CreatedBy = "anonymous";
            var userResponse = _mapper.Map<UserResponse>(await _userRepository.AddAsync(user));
            _logger.LogInformation($"Created user, {JsonSerializer.Serialize(userResponse)}.");

            return userResponse != null;
        }
    }
}