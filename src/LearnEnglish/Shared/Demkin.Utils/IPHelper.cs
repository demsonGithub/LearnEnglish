using System.Net.Sockets;

namespace Demkin.Utils
{
    public class IPHelper
    {
        public static bool TestConnection(string host, int port, int millisecondsTimeout)
        {
            TcpClient client = new TcpClient();
            try
            {
                var ar = client.BeginConnect(host, port, null, null);
                ar.AsyncWaitHandle.WaitOne(millisecondsTimeout);
                return client.Connected;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                client.Close();
            }
        }

        public static string GetIP(string conn)
        {
            try
            {
                string ip = conn.Split(';')[0].Split('=')[1].Split('\\')[0];
                return ip;
            }
            catch
            {
                return "0.0.0.0";
            }
        }
    }
}