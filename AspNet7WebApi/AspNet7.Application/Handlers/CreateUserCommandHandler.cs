using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Exceptions;
using AspNet7.Application.Common.Helpers;
using AspNet7.Application.Responses;
using AspNet7.Core.Entities;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAspNet7Logger<CreateUserCommandHandler> _logger;

        public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAspNet7Logger<CreateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
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
            _logger.Information($"Created user, {JsonSerializer.Serialize(userResponse)}.");

            return userResponse != null;
        }
    }
}