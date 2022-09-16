using MediatR;

namespace Demkin.System.WebApi.Application.Commands
{
    public class CreateAdminCommand : IRequest<string>
    {
        public string Password { get; }

        public CreateAdminCommand(string password)
        {
            Password = password;
        }
    }
}