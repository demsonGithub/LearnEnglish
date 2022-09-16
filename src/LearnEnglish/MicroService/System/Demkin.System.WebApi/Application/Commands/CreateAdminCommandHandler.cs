using Demkin.System.Domain.Entities;
using Demkin.System.Infrastructure.Repositories;
using MediatR;

namespace Demkin.System.WebApi.Application.Commands
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, string>
    {
        private readonly IUserRepository _userRepository;

        public CreateAdminCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            // 判断数据库存不存在
            //var isExist = await _userRepository.IsExistAsync(x => x.UserName == "admin");

            //if (isExist)
            //{
            //    return "已存在admin账号";
            //}
            var adminEntity = new User("admin", request.Password, null);
            var result = await _userRepository.AddAsync(adminEntity);

            var isSave = await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return isSave ? $"{result.UserName}添加成功，密码：{result.Password}" : "保存失败";
        }
    }
}