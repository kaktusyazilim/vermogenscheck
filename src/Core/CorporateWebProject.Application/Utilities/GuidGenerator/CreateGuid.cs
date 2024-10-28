using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Utilities.GuidGenerator
{
    public static class CreateGuid
    {
        public static string Create()
        {
            return Guid.NewGuid().ToString().ToUpper().Replace("Ş", "S").Replace("Ç", "C").Replace("Ü", "U").Replace("İ", "I").Replace("Ğ", "G").Replace("@", "").Replace("!", "").Replace("-", "").Replace(" ", "").Substring(0, 19).ToLower();
        }
        public static string CreateNumberGuid(int min, int max)
        {
            Random rnd = new Random();
            return rnd.Next(min, max).ToString();
        }
    }
}
