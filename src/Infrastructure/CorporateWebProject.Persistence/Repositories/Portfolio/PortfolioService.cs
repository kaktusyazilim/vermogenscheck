using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Persistence.Contexs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence.Repositories.Portfolio
{
    public class PortfolioService : BaseRepository<Domain.Entities.Portfolio>, IPortfolioRepository
    {
        public PortfolioService(ProjectContext db, IMemoryCache memoryCache) : base(db, memoryCache)
        {
        }
    }
}
