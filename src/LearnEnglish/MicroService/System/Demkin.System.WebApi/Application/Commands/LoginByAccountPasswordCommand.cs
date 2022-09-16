using MediatR;

namespace Demkin.System.WebApi.Application.Commands
{
    public class LoginByAccountPasswordCommand : IRequest<string>
    {
        public LoginByAccountPasswordCommand(string account, string password)
        {
            Account = account;
            Password = password;
        }

        public string Account { get; }
        public string Password { get; }
    }
}