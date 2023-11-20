using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using AutoMapper;
using AspNet7.Application.Commands;
using AspNet7.Application.Common.Helpers;
using AspNet7.Core.Logging;
using AspNet7.Core.Repository;
using AspNet7.Core.Specifications;

namespace AspNet7.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAspNet7Logger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAspNet7Logger<LoginUserCommandHandler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetSingleAsync(new UserSpecification(request.UserName));

            // check if username exists and the password is correct
            if ((user == null) || (!Password.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt)))
            {
                _logger.Error($"Username or password is incorrect");
                return false;
            }
            else
                return true;
        }
    }
}
