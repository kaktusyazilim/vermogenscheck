using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence.Contexs
{
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        protected abstract TContext CreateNewInstance(DbContextOptions<TContext> options);
        public TContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<TContext> builder = new DbContextOptionsBuilder<TContext>();
            builder.UseSqlServer("server=mssql.kaktusyazilim.com;database=UplifeAcademyDB;uid=kaktusadmin;pwd=ynyhqmdek35;Trusted_Connection=True;TrustServerCertificate=True;;Integrated Security=False;");
            //builder.EnableSensitiveDataLogging();

            return CreateNewInstance(builder.Options);
        }
    }
}
