using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.UnitOfWork;
using CorporateWebProject.Persistence.Contexs;
using CorporateWebProject.Persistence.Repositories;
using CorporateWebProject.Persistence.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Application.Repositories.SocialMediaPost;
using Microsoft.Extensions.Caching.Memory;
using CorporateWebProject.Application.Repositories.PortfolioCategoryMap;
using CorporateWebProject.Application.Repositories.Brands;
using CorporateWebProject.Persistence.Repositories.Portfolio;
using CorporateWebProject.Application.Repositories.Team;
using CorporateWebProject.Persistence.Repositories.Team;

namespace CorporateWebProject.Persistence
{
    public static class ServiceRegistrations
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ProjectContext>(options => options.UseSqlServer(Configurations.ConnectionString), ServiceLifetime.Scoped);

            services.AddTransient<IAuthorizationRepository, AuthorizationService>();
            services.AddTransient<IBlogRepository, BlogService>();
            services.AddTransient<ICategoryRepository, CategoryService>();
            services.AddTransient<IContactMessageRepository, ContactMessageService>();
            services.AddTransient<ICustomerCommentRepository, CustomerCommentService>();
            services.AddTransient<IFileRepository, FileService>();
            services.AddTransient<ILanguageRepository, LanguageService>();
            services.AddTransient<ILangValueRepository, LangValueService>();
            services.AddTransient<ILocationRepository, LocationService>();
            services.AddTransient<IModulRepository, ModulService>();
            services.AddTransient<IPageRepository, PageService>();
            services.AddTransient<IPageValueGroupRepository, PageValueGroupService>();
            services.AddTransient<IPageValueRepository, PageValueService>();
            services.AddTransient<IPageValueTypeRepository, PageValueTypeService>();
            services.AddTransient<IPartnerRepository, PartnerService>();
            services.AddTransient<IPersonnelRepository, PersonnelService>();
            services.AddTransient<IProjectRepository, ProjectService>();
            services.AddTransient<IServiceRepository, ServiceService>();
            services.AddTransient<ISettingRepository, SettingService>();
            services.AddTransient<ISliderRepository, SliderService>();
            services.AddTransient<ISmtpSettingRepository, SmtpSettingService>();
            services.AddTransient<ISocialMediaRepository, SocialMediaService>();
            services.AddTransient<ISubCategoryRepository, SubCategoryService>();
            services.AddTransient<ISubscriptionRepository, SubscriptionService>();
            services.AddTransient<ITypeRepository, TypeService>();
            services.AddTransient<IUserRepository, UserService>();
            services.AddTransient<IVisitorLogDetailRepository, VisitorLogDetailService>();
            services.AddTransient<IVisitorLogRepository, VisitorLogService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IBlogCategoryRepository, BlogCategoryService>();
            services.AddTransient<IBankRepository, BankService>();
            services.AddTransient<IInvoiceRepository, InvoiceService>();
            services.AddTransient<IInvoiceStatuRepository, InvoiceStatuService>();
            services.AddTransient<ICompanyRepository, CompanyService>();
            services.AddTransient<IInvoiceDetailRepository, InvoiceDetailService>();
            services.AddTransient<IGalleryRepository, GalleryService>();


            services.AddTransient<ICityRepository, CityService>();
            services.AddTransient<IDistrictRepository, DistrictService>();
            services.AddTransient<IPaymentRepository, PaymentService>();
            services.AddTransient<IJobRepository, JobService>();
            services.AddTransient<ICareerRepository, CareerService>();
            services.AddTransient<IFolderRepository, FolderService>();
            services.AddTransient<IDocumentRepository, DocumentService>();
            services.AddTransient<IFolderPermissionRepository, FolderPermissionService>();
            services.AddTransient<ILowestCategoryRepository, LowestCategoryService>();
            services.AddTransient<ICounterRepository, CounterService>();
            services.AddTransient<IProductRepository, ProductService>();

            services.AddTransient<IOfficialDocumentRepository, OfficialDocumentService>();
            services.AddTransient<IHistoryRepository, HistoryService>();
            services.AddTransient<INotificationRepository, NotificationService>();

            services.AddTransient<ICurrencyRepository, CurrencyService>();
            services.AddTransient<IEventRepository, EventService>();
            services.AddTransient<ICommentRepository, CommentService>();

            services.AddTransient<ISocialMediaPostRepository, SocialMediaPostService>();
            services.AddTransient<ISurveyRepository, SurveyService>();
            services.AddTransient<IQuestionRepository, QuestionService>();
            services.AddTransient<IAnswerRepository, AnswerService>();
            services.AddTransient<IInvoiceDeliveryRepository, InvoiceDeliveryService>();

            services.AddTransient<IAlarmRepository, AlarmService>();
            services.AddTransient<INoteRepository, NoteService>();
            services.AddTransient<IAccountRepository, AccountService>();
            services.AddTransient<IAccountTransactionRepository, AccountTransactionService>();
            services.AddTransient<ICrmSettingRepository, CrmSettingService>();

            services.AddTransient<IPortfolioCategoryMapRepository, PortfolioCategoryMapService>();
            services.AddTransient<IBrandRepository,BrandService>();

            services.AddTransient<IPortfolioRepository,PortfolioService>();
            services.AddTransient<IFaqRepository,FaqService>();

            services.AddTransient<ITeamRepository, TeamService>();

            services.AddTransient<IWhyUsRepository, WhyUsService>();
            services.AddTransient<IPacketRepository, PacketService>();
            services.AddTransient<IAboutUsRepository, AboutUsService>();
            services.AddTransient<IExchangeRepository, ExchangeService>();
            services.AddTransient<IContactCategoryRepository, ContactCategoryService>();
            services.AddTransient<IContentRepository, ContentService>();


        }
    }
}
