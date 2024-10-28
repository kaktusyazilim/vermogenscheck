﻿using CorporateWebProject.Application.Repositories;
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
    public class CategoryService : BaseRepository<Categories>, ICategoryRepository
    {
        public CategoryService(ProjectContext db, IMemoryCache memoryCache) : base(db, memoryCache)
        {
        }
    }
}