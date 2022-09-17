using Demkin.System.Domain.AggregatesModel.RoleAggregate;

namespace Demkin.System.WebApi.ViewModel.UserViewModels
{
    public class UserInfoViewModel
    {
        public string UserName { get; set; }

        public List<Role> Roles { get; set; }
    }
}