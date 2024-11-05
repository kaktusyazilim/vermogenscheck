using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using CorporateWebProject.Application.Context;
using CorporateWebProject.Application.Utilities.GuidGenerator;
using CorporateWebProject.Domain.Common;
using CorporateWebProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using CorporateWebProject.Persistence.Repositories;

namespace CorporateWebProject.Persistence.Contexs
{
    public class ProjectContext : DbContext, IApplicationContext
    {
        public DbSet<Authorizations> Authorizations { get; set; }
        public DbSet<Blogs> Blogs  { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ContactMessages> ContactMessages { get; set; }
        public DbSet<CustomerComments> CustomerComments { get; set; }
        public DbSet<Files> Files { get; set; }
        public DbSet<Languages> Languages  { get; set; }
        public DbSet<LangValues> LangValues { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Modules> Modules { get; set; }
        public DbSet<Pages> Pages { get; set; }
        public DbSet<PageValueGroups> PageValueGroups { get; set; }
        public DbSet<PageValues> PageValues { get; set; }
        public DbSet<PageValueTypes> PageValueTypes { get; set; }
        public DbSet<Partners> Partners { get; set; }
        public DbSet<Personnels> Personnels { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Services> Services { get; set; }
        public DbSet<Settings> Settings { get; set; }
        public DbSet<SmtpSettings> SmtpSettings { get; set; }
        public DbSet<SocialMedia> SocialMedia { get; set; }
        public DbSet<SubCategories> SubCategories { get; set; }
        public DbSet<Subscriptions> Subscriptions { get; set; }
        public DbSet<Types> Types { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<VisitorLogDetails> VisitorLogDetails { get; set; }
        public DbSet<VisitorLogs> VisitorLogs { get; set; }
        public DbSet<BlogCategories> BlogCategories { get; set; }
        public DbSet<Invoices> Invoices { get; set; }
        public DbSet<InvoiceStatus> InvoiceStatus { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
        public DbSet<Companies> Companies { get; set; }
        public DbSet<Banks> Banks { get; set; }
        public DbSet<Cities> Cities { get; set; }
        public DbSet<Districts> Districts { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Careers> Careers { get; set; }
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Documents> Documents { get; set; }
        public DbSet<Folders> Folders { get; set; }
        public DbSet<FolderPermissions> FolderPermissions { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<Sliders> Sliders { get; set; }
        public DbSet<OfficialDocuments> OfficialDocuments { get; set; }
        public DbSet<LowestCategory> LowestCategory { get; set; }
        public DbSet<Counters> Counters { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<History> History { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<SocialMediaPosts> SocialMediaPosts { get; set; }
        public DbSet<Surveys> Surveys { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<Answers> Answers { get; set; }
        public DbSet<InvoiceDeliveries> InvoiceDeliveries { get; set; }
        public DbSet<Notes> Notes { get; set; }
        public DbSet<Alarms> Alarms { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<AccountTransactions> AccountTransactions { get; set; }
        public DbSet<CrmSettings> CrmSettings { get; set; }
        public DbSet<PortfolioCategoryMap> PortfolioCategoryMap { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Portfolio> Portfolio { get; set; }
        public DbSet<Faq> Faq { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<WhyUs> WhyUs { get; set; }
        public DbSet<Packets> Packets { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
        public DbSet<Exchanges> Exchanges { get; set; }
        public DbSet<ContactCategories> ContactCategories { get; set; }
        public DbSet<Contents> Contents { get; set; }

        public ProjectContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configurations.ConnectionString);
            optionsBuilder.EnableSensitiveDataLogging(false);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(t => t.GetProperties())
                     .Where(p => p.ClrType == typeof(decimal)))
            {

                property.SetColumnType("decimal(18, 2)");  // Varsayılan olarak decimal(18,2) kullanır
            }

            
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<EntityBase>();
            foreach (var entity in datas)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreateDate = DateTime.Now;
                    entity.Entity.ModifiedDate = DateTime.Now;
                    entity.Entity.ItemGuid = CreateGuid.Create();
                    entity.Entity.IsPassive = false;
                    entity.Entity.IsDeleted = false;
                }
                else if (entity.State == EntityState.Modified)
                    entity.Entity.ModifiedDate = DateTime.Now;
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
