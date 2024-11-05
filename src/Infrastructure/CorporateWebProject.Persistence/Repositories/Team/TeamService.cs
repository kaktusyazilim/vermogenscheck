using CorporateWebProject.Application.Repositories.Team;
using CorporateWebProject.Persistence.Contexs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence.Repositories.Team
{
    public class TeamService : BaseRepository<Domain.Entities.Team>, ITeamRepository
    {
        public TeamService(ProjectContext db, IMemoryCache memoryCache) : base(db, memoryCache)
        {
        }
    }
}
