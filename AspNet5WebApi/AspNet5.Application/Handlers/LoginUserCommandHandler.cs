using System;
using System.Threading;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using MediatR;
using AutoMapper;
using AspNet5.Core.Logging;
using AspNet5.Core.Specifications;
using AspNet5.Core.Repository;
using AspNet5.Application.Common.Helpers;
using AspNet5.Application.Commands;

namespace AspNet5.Application.Handlers
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IAspNet5Logger<LoginUserCommandHandler> _logger;
        public LoginUserCommandHandler(IUserRepository userRepository, IMapper mapper, IAspNet5Logger<LoginUserCommandHandler> logger)
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
                _logger.LogError($"Username or password is incorrect");
                return false;
            }
            else
                return true;
        }
    }
}
