using System.Security.Cryptography;
using System.Text;

namespace Demkin.Utils
{
    public static class HashHelper
    {
        private static string ToHashString(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }

        public static string ComputeSha256Hash(Stream stream)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(stream);
                // 设定起始位置，否则在使用Stream保存的文件是空的
                stream.Position = 0;
                return ToHashString(bytes);
            }
        }

        public static string ComputeSha256Hash(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ToHashString(bytes);
            }
        }

        public static string ComputeMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return ToHashString(bytes);
            }
        }

        public static string ComputeMd5Hash(Stream input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] bytes = md5Hash.ComputeHash(input);
                input.Position = 0;
                return ToHashString(bytes);
            }
        }
    }
}