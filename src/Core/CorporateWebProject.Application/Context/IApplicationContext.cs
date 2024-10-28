using Microsoft.EntityFrameworkCore;
using CorporateWebProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Context
{
    public interface IApplicationContext
    {
        DbSet<Authorizations> Authorizations { get; set; }
        DbSet<Blogs> Blogs { get; set; }
        DbSet<Categories> Categories { get; set; }
        DbSet<ContactMessages> ContactMessages { get; set; }
        DbSet<CustomerComments> CustomerComments { get; set; }
        DbSet<Files> Files { get; set; }
        DbSet<Languages> Languages { get; set; }
        DbSet<LangValues> LangValues { get; set; }
        DbSet<Locations> Locations { get; set; }
        DbSet<Modules> Modules { get; set; }
        DbSet<Pages> Pages { get; set; }
        DbSet<PageValueGroups> PageValueGroups { get; set; }
        DbSet<PageValues> PageValues { get; set; }
        DbSet<PageValueTypes> PageValueTypes { get; set; }
        DbSet<Partners> Partners { get; set; }
        DbSet<Personnels> Personnels { get; set; }
        DbSet<Projects> Projects { get; set; }
        DbSet<Services> Services { get; set; }
        DbSet<Settings> Settings { get; set; }
        DbSet<SmtpSettings> SmtpSettings { get; set; }
        DbSet<SocialMedia> SocialMedia { get; set; }
        DbSet<SubCategories> SubCategories { get; set; }
        DbSet<Subscriptions> Subscriptions { get; set; }
        DbSet<Types> Types { get; set; }
        DbSet<Users> Users { get; set; }
        DbSet<VisitorLogDetails> VisitorLogDetails { get; set; }
        DbSet<VisitorLogs> VisitorLogs { get; set; }

    }
}
