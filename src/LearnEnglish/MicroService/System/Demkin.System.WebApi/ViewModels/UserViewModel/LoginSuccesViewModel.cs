namespace Demkin.System.WebApi.ViewModels.UserViewModel
{
    public class LoginSuccesViewModel
    {
        public long UserId { get; set; }

        public string Name { get; set; }

        public string Token { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}