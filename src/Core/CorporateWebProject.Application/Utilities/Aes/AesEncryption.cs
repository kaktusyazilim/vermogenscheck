using Newtonsoft.Json;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CorporateWebProject.Application.Utilities
{
    public static class AesEncryption<T>
    {
        // Şifreleme ve çözme işlemleri için anahtar ve IV (Initialization Vector)
        private static readonly byte[] Key = Encoding.UTF8.GetBytes("CACTUSSOFTWARE12"); // 16 byte, AES-128 için
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("01092020CACTUS12"); // 16 byte, AES-128 için

        // Metni AES ile şifreler
        public static string Encrypt(T data)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(JsonConvert.SerializeObject(data));
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        // Şifrelenmiş metni AES ile çözer
        public static T Decrypt(string cipherText)
        {
            try
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;

                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                var result = srDecrypt.ReadToEnd();
                                return JsonConvert.DeserializeObject<T>(result);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                return default(T);

                throw;
            }

        }


    }
}
