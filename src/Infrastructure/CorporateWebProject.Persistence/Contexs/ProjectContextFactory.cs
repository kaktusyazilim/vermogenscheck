using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence.Contexs
{
    public class ProjectContextFactory : DesignTimeDbContextFactory<ProjectContext>
    {
        protected override ProjectContext CreateNewInstance(DbContextOptions<ProjectContext> options)
        {
            return new ProjectContext(options);
        }
    }
}
