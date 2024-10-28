using CorporateWebProject.Application.Repositories.SocialMediaPost;
using CorporateWebProject.Domain.Entities;
using CorporateWebProject.Persistence.Contexs;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence.Repositories
{
    public class SocialMediaPostService : BaseRepository<SocialMediaPosts>, ISocialMediaPostRepository
    {
        public SocialMediaPostService(ProjectContext db, IMemoryCache memoryCache) : base(db, memoryCache)
        {
        }
    }
}
