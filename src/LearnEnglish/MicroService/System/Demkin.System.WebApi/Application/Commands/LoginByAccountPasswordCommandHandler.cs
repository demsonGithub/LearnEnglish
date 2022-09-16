using Demkin.System.Infrastructure.Repositories;
using MediatR;

namespace Demkin.System.WebApi.Application.Commands
{
    public class LoginByAccountPasswordCommandHandler : IRequestHandler<LoginByAccountPasswordCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public LoginByAccountPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginByAccountPasswordCommand request, CancellationToken cancellationToken)
        {
            // 核对账号密码是否正确
            var userEntity = await _userRepository.GetAsync(item => item.UserName == request.Account && item.Password == request.Password);

            if (userEntity == null)
            {
                return "";
            }

            return userEntity.UserName;
        }
    }
}