using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Exceptions;
using AspNet7.Application.Common.Identity;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrentUser _currentUser;
        private readonly IAspNet7Logger<DeleteUserCommandHandler> _logger;

        public DeleteUserCommandHandler(IUserRepository userRepository, ICurrentUser currentUser, IAspNet7Logger<DeleteUserCommandHandler> logger)
        {
            _userRepository = userRepository;
            _currentUser = currentUser;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingleAsync(new UserSpecification(request.UserId));
            if (user == null)
                throw new BadRequestException($"User with userId, {request.UserId} does not exist.");

            user.LastModifiedBy = _currentUser.UserName;
            user.IsDeleted = true;
            var result = await _userRepository.UpdateAsync(user);
            _logger.Information($"Deleted user with userId, {request.UserId}.");
            return result != null;
        }
    }
}
