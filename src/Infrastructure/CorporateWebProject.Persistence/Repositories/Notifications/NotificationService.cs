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
    public class NotificationService : BaseRepository<Notifications>, INotificationRepository
    {
        public NotificationService(ProjectContext db, IMemoryCache memoryCache) : base(db, memoryCache)
        {
        }
    }
}
