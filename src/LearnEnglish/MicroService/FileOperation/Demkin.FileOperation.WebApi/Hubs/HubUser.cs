namespace Demkin.FileOperation.WebApi.Hubs
{
    public class HubUser
    {
        /// <summary>
        /// 连接ID
        /// </summary>
        public string ConnectionID { get; set; }

        public HubUser(string connectionId)
        {
            this.ConnectionID = connectionId;
        }
    }
}