using Microsoft.AspNetCore.SignalR;

namespace Demkin.FileOperation.WebApi.Hubs
{
    public class FileUploadStatusHub : Hub
    {
        // 创建用户集合
        public static List<string> users = new List<string>();

        public override Task OnConnectedAsync()
        {
            // 查询用户
            var userId = users.Where(u => u == Context.ConnectionId).FirstOrDefault();
            // 判断是否存在，否则添加到集合
            if (string.IsNullOrEmpty(userId))
            {
                users.Add(Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            users.Remove(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }
    }
}