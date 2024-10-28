using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.Security
{
    public class HashingHelper
    {
        public static string CreatePasswordHash(string password, string Mail)
        {
            using (SHA256Managed sha = new())
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(password + Mail);
                byte[] result = sha.ComputeHash(data);
                StringBuilder output = new StringBuilder("");
                for (int i = 0; i < result.Length; i++)
                {
                    output.Append(result[i].ToString("X2"));
                }
                return output.ToString();
            }
        }
        public static bool VerifyPasswordHash(string password, string mail, string passwordHash)
        {
            using (SHA256Managed sha = new())
            {
                byte[] data = UTF8Encoding.UTF8.GetBytes(password + mail);
                byte[] result = sha.ComputeHash(data);
                StringBuilder output = new StringBuilder("");
                for (int i = 0; i < result.Length; i++)
                {
                    output.Append(result[i].ToString("X2"));
                }
                if (output.ToString() == passwordHash)
                    return true;
                return false;
            }
        }
        public static string GetSha256Hash(string input)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                var builder = new StringBuilder();
                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
