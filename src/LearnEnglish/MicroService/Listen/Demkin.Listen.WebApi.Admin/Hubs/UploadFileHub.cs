using Microsoft.AspNetCore.SignalR;

namespace Demkin.Listen.WebApi.Admin.Hubs
{
    public class UploadFileHub : Hub
    {
        // 创建用户集合
        public static List<string> _connections = new List<string>();

        public override Task OnConnectedAsync()
        {
            string connId = Context.ConnectionId;

            // 判断是否存在，否则添加到集合
            if (!_connections.Contains(connId))
            {
                _connections.Add(connId);
            }
            // 把当前连接的Id返回给前端
            Clients.Client(connId).SendAsync("ConnectCallback", connId);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            string connId = Context.ConnectionId;
            _connections.Remove(connId);

            return base.OnDisconnectedAsync(exception);
        }
    }
}