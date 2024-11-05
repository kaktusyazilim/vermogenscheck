using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence
{
    public static class Configurations
    {
        public static string ConnectionString
        {
            get
            {
                //ConfigurationManager configurationManager = new();
                //configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../CorporateWebProject.WebAPI"));
                //configurationManager.AddJsonFile("appsettings.json");
                //return configurationManager.GetConnectionString("Mssql")!;
                return "Server=mssql.kaktusdns.com;Database=VermogenscheckDB;User ID=vermogenscheck;Password=@Vermogenscheck2024!;Max Pool Size=200;Connect Timeout=30;Encrypt=True;TrustServerCertificate=True;"
;

                return "server=mssql.kaktusyazilim.com;database=UplifeAcademyDB;uid=kaktusadmin;pwd=ynyhqmdek35;Trusted_Connection=True;TrustServerCertificate=True;;Integrated Security=False;";
            }
        }
    }
}
