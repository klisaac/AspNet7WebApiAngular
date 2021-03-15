using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using AutoMapper;
using MediatR;
using AspNet5.Application.Commands;
using AspNet5.Application.Common.Identity;
using AspNet5.Application.Common.Exceptions;
using AspNet5.Core.Logging;
using AspNet5.Core.Repository;
using AspNet5.Core.Specifications;
using AspNet5.Application.Common.Helpers;
using AspNet5.Application.Responses;

namespace AspNet5.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<UpdateUserCommandHandler> _logger;

        public UpdateUserCommandHandler(IUserRepository userRepository, ICurrentUser currentUser, IMapper mapper, IAspNet5Logger<UpdateUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingleAsync(new UserSpecification(request.UserId));
            if (user == null)
                throw new BadRequestException($"User with user, {request.UserId} does not exist.");

            // throw error if the new username is already taken
            if (await _userRepository.GetSingleAsync(new UserSpecification(request.UserName)) != null)
            {
                _logger.LogInformation($"User, {request.UserName} already exists.");
                throw new BadRequestException($"User, {request.UserName} already exists.");
            }
            var passwordSaltAndHash = Password.CreatePasswordHash(request.Password);

            // update user properties
            user.UserName = request.UserName;
            user.PasswordSalt = passwordSaltAndHash.Item1;
            user.PasswordHash = passwordSaltAndHash.Item2;
            user.LastModifiedBy = _currentUser.UserName;
            var userResponse = _mapper.Map<UserResponse> (await _userRepository.UpdateAsync(user));
            _logger.LogInformation($"Updated user, {JsonSerializer.Serialize(userResponse)}.");

            return userResponse != null;
        }
    }
}