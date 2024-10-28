using Azure;
using CorporateWebProject.Application.Dto.Authorization;
using CorporateWebProject.Application.Dto.Contract;
using CorporateWebProject.Application.Dto.Payment;
using CorporateWebProject.Application.Dto.Proposal;
using CorporateWebProject.Application.Dto.SchoolFilter;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities;
using CorporateWebProject.Application.Utilities.Cache;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.WebUI.Models.Service;
using CP.VPOS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Identity.Client;
using NetsisService;
using System.Globalization;
using System.Linq;

namespace CorporateWebProject.WebUI.Models
{
    public class ServiceVM : IServiceVM
    {

        private readonly IMemoryCache _memoryCache;
        private readonly HttpContext _context;

        public ServiceVM(HttpContext context, IMemoryCache memoryCache)
        {
            _context = context;

            _memoryCache = memoryCache;

            // Sadece dil ve kültür ayarlarını constructor'da yükleyelim
            CultureInfo.CurrentCulture = new CultureInfo("tr-TR");
        }
        public ServiceVM()
        {

        }

        public async Task LoadLanguageSettingsAsync(HttpContext context,string language = "tr")
        {
            string cacheKey = $"Language_{language}";
            CacheManager.AddCacheKey(cacheKey);
            if (!_memoryCache.TryGetValue(cacheKey, out Languages currentLanguage))
            {
                var languageRepository = (ILanguageRepository)_context.RequestServices.GetService(typeof(ILanguageRepository));
                currentLanguage = (await languageRepository.Get(x => x.Code.ToLower() == language.ToLower() || x.CultureCode.ToLower() == language.ToLower())).Data;

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
                };
                _memoryCache.Set(cacheKey, currentLanguage, cacheOptions);
            }
            this.CurrentLanguage = currentLanguage;

            // Ayarları cache'den getir
            string settingCacheKey = $"Settings_{this.CurrentLanguage.Id}";
            CacheManager.AddCacheKey(settingCacheKey);

            if (!_memoryCache.TryGetValue(settingCacheKey, out Settings currentSettings))
            {
                var cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
                };
                var settingRepository = (ISettingRepository)_context.RequestServices.GetService(typeof(ISettingRepository));
                currentSettings = (await settingRepository.Get(x => x.LangId == this.CurrentLanguage.Id)).Data;

                _memoryCache.Set(settingCacheKey, currentSettings, cacheOptions);
            }
            this.CurrentSettings = currentSettings;

            // Translate verilerini cache'e al
            string translateCacheKey = $"Translate_{this.CurrentLanguage.CultureCode}";
            CacheManager.AddCacheKey(translateCacheKey);

            if (!_memoryCache.TryGetValue(translateCacheKey, out Dictionary<string, string> translateDictionary))
            {
                var languageValueRepository = (ILangValueRepository)_context.RequestServices.GetService(typeof(ILangValueRepository));
                translateDictionary = (await languageValueRepository.GetListAsync(x => x.CultureCode == this.CurrentLanguage.Code)).Data.ToDictionary(x => x.Name, x => x.Value);

                _memoryCache.Set(translateCacheKey, translateDictionary, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
                });
            }

            // Translate dictionary'sini ayarla
            this.Translate = translateDictionary;
        }



        public async Task LoadCityAndDistrictDataAsync(HttpContext context)
        {
            if (_cityListLazy == null || _districtListLazy == null)
            {
                var services = _context.RequestServices;
                var cityRepository = (ICityRepository)services.GetService(typeof(ICityRepository));
                var districtRepository = (IDistrictRepository)services.GetService(typeof(IDistrictRepository));

                _cityListLazy = new Lazy<Task<List<Cities>>>(async () => (await cityRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data);
                _districtListLazy = new Lazy<Task<List<Districts>>>(async () => (await districtRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data);

                // Şehir ve ilçe verilerini ilk çağrıldığında yükle
                CityList = (await _cityListLazy.Value).ToList();
                DistrictList = await _districtListLazy.Value;
            }
        }

        // Kullanıcı Verilerini Yükle
        public async Task LoadUserDataAsync(HttpContext context)
        {
            var CURRENT_SESSION = context.Request.Cookies["CURRENT_SESSION"];
            this.CurrentUser = AesEncryption<Users>.Decrypt(CURRENT_SESSION);
            
            
        }


        // Modul ve Page Verilerini Yükleme (Kontrol Panelinde Kullanılıyor)
        public async Task LoadModulAndPageDataAsync(HttpContext context)
        {
            var services = _context.RequestServices;
            var modulRepository = (IModulRepository)services.GetService(typeof(IModulRepository));
            var pageRepository = (IPageRepository)services.GetService(typeof(IPageRepository));
            var paths = _context.Request.Path.Value.Split('/').Where(x => x != "").ToList();

            if (paths.Count > 1)
            {
                string moduleCacheKey = $"Modules_single_{paths[1]}";
                CacheManager.AddCacheKey(moduleCacheKey);

                if (!_memoryCache.TryGetValue(moduleCacheKey, out Modules currentModule))
                {
                    var pathValue = paths[1];

                    currentModule = (await modulRepository.Get(x => x.Url == pathValue)).Data;
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };
                    _memoryCache.Set(moduleCacheKey, currentModule, cacheOptions);
                }
                if(currentModule!=null && currentModule.Url != paths[1])
                {
                    var pathValue = paths[1];
                    currentModule = (await modulRepository.Get(x => x.Url == pathValue)).Data;
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };
                    _memoryCache.Set(moduleCacheKey, currentModule, cacheOptions);
                }
                this.Modul = currentModule;
            }

            if (this.Modul != null)
            {
                string pageCacheKey = $"Pages_single_{this.Modul.Id}";
                var result = _memoryCache.TryGetValue(pageCacheKey, out Pages currentPage);
                CacheManager.AddCacheKey(pageCacheKey);

                if (result == true && currentPage == null)
                {
                    currentPage = (await pageRepository.Get(x => x.ModulId == this.Modul.Id)).Data;
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };
                    _memoryCache.Set(pageCacheKey, currentPage, cacheOptions);
                }
                this.Page = currentPage;

            }
            if (this.PageList == null)
            {
                string pageCacheKey = $"Pages_list_001";
                CacheManager.AddCacheKey(pageCacheKey);

                if (!_memoryCache.TryGetValue(pageCacheKey, out List<Pages> pageList))
                {
                    // Cache'de yoksa veriyi veritabanından alıyoruz
                    pageList = (await pageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

                    // Cache ayarlarını belirliyoruz
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };

                    // Cache'e ekliyoruz
                    _memoryCache.Set(pageCacheKey, pageList, cacheOptions);
                }
                if(pageList==null)
                {
                    // Cache'de yoksa veriyi veritabanından alıyoruz
                    pageList = (await pageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x=>x.Queue).ToList();

                    // Cache ayarlarını belirliyoruz
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };

                    // Cache'e ekliyoruz
                    _memoryCache.Set(pageCacheKey, pageList, cacheOptions);
                }
                // Veriyi PageList'e atıyoruz
                this.PageList = pageList;
            }

            if (this.ModulList == null)
            {
                string modulCacheKey = $"Modules_list"; // Cache key isimlendirmesi
                CacheManager.AddCacheKey(modulCacheKey);

                if (!_memoryCache.TryGetValue(modulCacheKey, out List<Modules> modulList))
                {
                    // Cache'de yoksa veriyi veritabanından alıyoruz
                    modulList = (await modulRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x=>x.Queue).ToList();

                    // Cache ayarlarını belirliyoruz
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };

                    // Cache'e ekliyoruz
                    _memoryCache.Set(modulCacheKey, modulList, cacheOptions);
                }
                if(modulList==null)
                {
                    // Cache'de yoksa veriyi veritabanından alıyoruz
                    modulList = (await modulRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data;

                    // Cache ayarlarını belirliyoruz
                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6) // 6 saat boyunca cache'de tut
                    };

                    // Cache'e ekliyoruz
                    _memoryCache.Set(modulCacheKey, modulList, cacheOptions);
                }
                // Veriyi ModulList'e atıyoruz
                this.ModulList = modulList;
            }

            if (this.LanguageList == null)
            {
                string languageCacheKey = $"Languages_list";
                CacheManager.AddCacheKey(languageCacheKey);

                if (!_memoryCache.TryGetValue(languageCacheKey, out List<Languages> languageList))
                {
                    var languageRepository = (ILanguageRepository)services.GetService(typeof(ILanguageRepository));
                    languageList = (await languageRepository.GetListAsync(x => x.IsPassive == false && x.IsDeleted == false)).Data.OrderBy(x => x.Id).ToList();

                    var cacheOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)
                    };
                    _memoryCache.Set(languageCacheKey, languageList, cacheOptions);
                }
                this.LanguageList = languageList;
            }

            // Tür (Type) ve diğer veriler için Lazy Loading ve Cache kullanımı
            var typeRepository = (ITypeRepository)services.GetService(typeof(ITypeRepository));
            if (this.Modul != null && this.Type==null)
            {
                this.Type = (await typeRepository.Get(x => x.IsPassive == false && x.IsDeleted == false && x.ModulId == this.Modul.Id)).Data;
            }
        }

        public async Task FillDataAsync(HttpContext context)
        {
            try
            {
                var services = context.RequestServices;

                // Dil ve Ayar Verilerini Cache'den Al
                await LoadLanguageSettingsAsync(context, context.Request.Cookies["language"]);

                // Kullanıcı Verilerini Yükle (Kullanıcı giriş yaptıysa)
                var user = context.Request.Cookies["userGuid"];
                var client = context.Request.Cookies["clientGuid"];
                await LoadUserDataAsync(context);

                // Modül ve Sayfa Verilerini Cache'den Yükle (Eğer kontrol paneli içindeyseniz)
                if (context.Request.Path.Value.StartsWith("/manager"))
                {
                    await LoadModulAndPageDataAsync(context);
                }

                // Şirket verilerini yükle (Eğer kullanıcı şirketle ilişkiliyse)
                if (!string.IsNullOrEmpty(client))
                {
                    var companyRepository = (ICompanyRepository)services.GetService(typeof(ICompanyRepository));
                    this.CurrentCompany = (await companyRepository.Get(x => x.ItemGuid == client)).Data;
                }

                // Bildirimleri yükle (Eğer şirketle ilgiliyse)
                var notificationRepository = (INotificationRepository)services.GetService(typeof(INotificationRepository));
                if (this.CurrentCompany != null)
                {
                    this.CurrentNotificationList = (await notificationRepository.GetListAsync(x => x.CompanyGuid == this.CurrentCompany.ItemGuid && x.IsPassive == false && x.IsDeleted == false)).Data;
                }

                // Tür (Type) ve diğer veriler için Lazy Loading ve Cache kullanımı
                var typeRepository = (ITypeRepository)services.GetService(typeof(ITypeRepository));
                if (this.Modul != null)
                {
                    this.Type = (await typeRepository.Get(x => x.IsPassive == false && x.IsDeleted == false && x.ModulId == this.Modul.Id)).Data;
                }
                else
                {
                    this.Type = (await typeRepository.GetListAsync(x => x.IsPage == false && x.IsDeleted == false)).Data.FirstOrDefault();
                }

                // Şehir ve ilçe verileri için Lazy Loading kullanılacak, ihtiyaç olduğunda çağrılır
            }
            catch (Exception ex)
            {
                // Hata yönetimi
                _context.Response.Redirect("/manager/500/" + ex.Message);
                await _context.Response.CompleteAsync();
            }
        }




        // Şehir ve ilçe verileri sadece ihtiyaç olduğunda yüklenir
        private Lazy<Task<List<Cities>>> _cityListLazy;
        private Lazy<Task<List<Districts>>> _districtListLazy;

        // Şehir ve ilçe verileri sadece ihtiyaç duyulduğunda yüklenecek
        public List<Cities> CityList { get; private set; }
        public List<Districts> DistrictList { get; private set; }

        // Dil ayarlarını cache'de tutarak veritabanına her seferinde sorgu yapmıyoruz
        public Languages CurrentLanguage { get; private set; }
        public Settings CurrentSettings { get; private set; }
        public Users CurrentUser { get; set; }

        // DTO
        public string Error { get; set; }

        public Dictionary<string, string> Translate { get; set; }

        public Modules NewModul { get; set; }
        public List<Modules> NewModulList { get; set; }
        public Modules Modul { get; set; }
        public List<Modules> ModulList { get; set; }

        public Pages Page { get; set; }
        public List<Pages> PageList { get; set; }

        public Users User { get; set; }
        public List<Users> UserList { get; set; }

        public Languages Language { get; set; }
        public List<Languages> LanguageList { get; set; }

        public Blogs Blog { get; set; }
        public List<Blogs> BlogList { get; set; }

        public CorporateWebProject.Domain.Entities.Types Type { get; set; }
        public List<CorporateWebProject.Domain.Entities.Types> TypeList { get; set; }

        public Categories Category { get; set; }
        public List<Categories> CategoryList { get; set; }

        public SubCategories SubCategory { get; set; }
        public List<SubCategories> SubCategoryList { get; set; }
        public SubCategories CountryCategory { get; set; }


        public LowestCategory LowestCategory { get; set; }
        public List<LowestCategory> LowestCategoryList { get; set; }
        public LowestCategory CityCategory { get; set; }


        public Authorizations Authorization { get; set; }
        public List<Authorizations> AuthorizationList { get; set; }
        public List<Authorizations> CurrentAuthorizationList { get; set; }
        public FileInfo[] LocalFiles { get; set; }

        public Services Service { get; set; }
        public List<Services> ServiceList { get; set; }

        public Invoices Invoice { get; set; }
        public List<Invoices> InvoiceList { get; set; }
        public InvoiceStatus InvoiceStatu { get; set; }
        public List<InvoiceStatus> InvoiceStatuList { get; set; }
        public InvoiceDetails InvoiceDetail { get; set; }
        public List<InvoiceDetails> InvoiceDetailList { get; set; }
        public Banks Bank { get; set; }
        public List<Banks> BankList { get; set; }
        public Companies Company { get; set; }
        public Companies CurrentCompany { get; set; }
        public List<Companies> CompanyList { get; set; }
        public List<Gallery> GalleryList { get; set; }
        public Gallery Gallery { get; set; }

        public Districts District { get; set; }
        public Cities City { get; set; }
        public LoginDTO LoginDTO { get; set; }

        public Settings Setting { get; set; }
        public List<Settings> SettingList { get; set; }
        public SmtpSettings SmtpSetting { get; set; }
        public SmtpSettings CurrentSmtpSetting { get; set; }
        public List<SmtpSettings> SmtpSettingList { get; set; }

        public ContactMessages Message { get; set; }
        public List<ContactMessages> MessageList { get; set; }

        public Payments Payment { get; set; }
        public List<Payments> PaymentList { get; set; }

        public SaleResponse SaleResponse { get; set; }

        public CreditCard CreditCard { get; set; }

        public Jobs Job { get; set; }
        public List<Jobs> JobList { get; set; }
        public Careers Career { get; set; }
        public List<Careers> CareerList { get; set; }

        public Documents Document { get; set; }
        public List<Documents> DocumentList { get; set; }
        public Folders Folder { get; set; }
        public List<Folders> FolderList { get; set; }
        public List<Folders> SubFolderList { get; set; }
        public Folders SubFolder { get; set; }

        public FolderPermissions FolderPermission { get; set; }
        public List<FolderPermissions> FolderPermissionList { get; set; }
        public List<FolderPermissions> CurrentFolderPermissionList { get; set; }
        public Sliders Slider { get; set; }
        public List<Sliders> SliderList { get; set; }

        public OfficialDocuments OfficialDocument { get; set; }
        public List<OfficialDocuments> OfficialDocumentList { get; set; }
        public Counters Counter { get; set; }
        public List<Counters> CounterList { get; set; }

        public List<Categories> SelectedCompanyCategories { get; set; }
        public Products Product { get; set; }
        public List<Products> ProductList { get; set; }
        public History History { get; set; }
        public List<History> HistoryList { get; set; }
        public Notifications Notification { get; set; }
        public List<Notifications> NotificationList { get; set; }
        public List<Notifications> CurrentNotificationList { get; set; }




        public Currency Currency { get; set; }
        public List<Currency> CurrencyList { get; set; }




        public Events Event { get; set; }
        public List<Events> EventList { get; set; }


        public Comments Comment { get; set; }
        public List<Comments> CommentList { get; set; }

        public SocialMediaPosts SocialMediaPost { get; set; }
        public List<SocialMediaPosts> SocialMediaPostList { get; set; }

        public SocialMedia SocialMedia { get; set; }
        public List<SocialMedia> SocialMediaList { get; set; }




        public SchoolFilterDTO SchoolFilter { get; set; }






        public InvoiceDeliveries InvoiceDelivery { get; set; }
        public List<InvoiceDeliveries> InvoiceDeliveryList { get; set; }


        public Notes Note { get; set; }
        public List<Notes> NoteList { get; set; }

        public Alarms Alarms { get; set; }
        public List<Alarms> AlarmList { get; set; }

        public Accounts Account { get; set; }
        public List<Accounts> AccountList { get; set; }
        public AccountTransactions AccountTransaction { get; set; }
        public List<AccountTransactions> AccountTransactionList { get; set; }



        public CrmSettings CurrentCrmSettings { get; set; }
        public CrmSettings CrmSettings { get; set; }


        public Brands Brand { get; set; }
        public List<Brands> BrandList { get; set; }

        public Portfolio Portfolio { get; set; }
        public List<Portfolio> PortfolioList { get; set; }

        public PortfolioCategoryMap PortfolioCategoryMap { get; set; }
        public List<PortfolioCategoryMap> PortfolioCategoryMapList { get; set; }

        public ContactDTO ContactDTO { get; set; }


        public Faq Faq { get; set; }
        public List<Faq> FaqList { get; set; }

        public Team Team { get; set; }
        public List<Team> TeamList { get; set; }

        public WhyUs WhyUs { get; set; }
        public List<WhyUs> WhyUsList { get; set; }
        public Packets Packet { get; set; }
        public List<Packets> PacketList { get; set; }

        public ProposalDTO Proposal { get; set; }

        public AboutUs AboutUs { get; set; }
        public List<AboutUs> AboutUsList { get; set; }


        public List<string> StringList { get; set; }



    }



}
