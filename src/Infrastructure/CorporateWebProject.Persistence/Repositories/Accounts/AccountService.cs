using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Persistence.Contexs;
using Microsoft.Extensions.Caching.Memory;

namespace CorporateWebProject.Persistence.Repositories
{
    public class AccountService : BaseRepository<Accounts>, IAccountRepository
    {
        public AccountService(ProjectContext db, IMemoryCache memoryCache)
            : base(db, memoryCache)
        {
        }
    }
}
