using System;
using System.Text;

namespace ForretasAPITester.Helpers
{
    public class SecurityHelper : IDisposable
    {
        private const string SALT = "S0m3WS_@na0m";

        public void Dispose() { }

        public string EncryptText(string text)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes($"{SALT}{text}");
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public SecurityHelper() { }
    }
}
