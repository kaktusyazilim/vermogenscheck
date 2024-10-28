using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using CorporateWebProject.Application.Exceptions;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.Utilities.ExceptionHelpers;
using CorporateWebProject.Application.Wrappers.Abstract;
using CorporateWebProject.Application.Wrappers.Concrete;
using CorporateWebProject.Domain.Common;
using CorporateWebProject.Persistence.Contexs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using CorporateWebProject.Application.Parameters;
using CorporateWebProject.Application.Utilities.ExpressionNormalizer;
using CorporateWebProject.Application.Utilities.Security;
using CorporateWebProject.Application.Utilities.Cache;

namespace CorporateWebProject.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
    {
        private readonly ProjectContext _db;
        private readonly IMemoryCache _memoryCache;

        public BaseRepository(ProjectContext db, IMemoryCache memoryCache)
        {
            _db = db;
            _memoryCache = memoryCache;
        }

        public DbSet<T> Table => _db.Set<T>();

        // GetListAsync metodu - Geri çağırma fonksiyonu ile cache yönetimi
        public async Task<IDataResult<List<T>>> GetListAsync(Expression<Func<T, bool>>? filter = null, bool tracking = false)
        {
            string filterString = filter != null ? ExpressionNormalizer.NormalizeExpression(filter) : "NoFilter";
            string filterHash = HashingHelper.GetSha256Hash(filterString); // Filtreyi hash'liyoruz
            var cacheKey = $"{typeof(T).Name}_list_{filterHash}";

            // Cache key'i listeye ekliyoruz
            CacheManager.AddCacheKey(cacheKey);

            if (_memoryCache.TryGetValue(cacheKey, out List<T> cachedList))
            {
                return new SuccessDataResult<List<T>>(cachedList);
            }

            try
            {
                var query = Table.AsQueryable();
                if (!tracking) query = query.AsNoTracking();

                var result = filter != null ? await query.Where(filter).ToListAsync() : await query.ToListAsync();

                // Cache'i geri çağırma fonksiyonu ile ekliyoruz
                _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), // 10 dakika cache süresi
                    PostEvictionCallbacks =
                    {
                        new PostEvictionCallbackRegistration
                        {
                            EvictionCallback = (key, value, reason, state) =>
                            {
                                CacheManager.RemoveCacheKey((string)key);
                            }
                        }
                    }
                });

                return new SuccessDataResult<List<T>>(result, ResponseMessage.SuccessMessage);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<List<T>>(ExceptionHelper.GetErrorMessage(ex));
            }
        }

        // Get metodu - Geri çağırma fonksiyonu ile cache yönetimi
        public async Task<IDataResult<T>> Get(Expression<Func<T, bool>> filter, bool tracking = false)
        {
            string filterString = ExpressionNormalizer.NormalizeExpression(filter);
            string filterHash = HashingHelper.GetSha256Hash(filterString);
            var cacheKey = $"{typeof(T).Name}_single_{filterHash}";
            // Cache key'i listeye ekliyoruz
            CacheManager.AddCacheKey(cacheKey);
            if (_memoryCache.TryGetValue(cacheKey, out T cachedEntity))
            {
                return new SuccessDataResult<T>(cachedEntity);
            }
         
            try
            {
                var query = Table.AsQueryable();
                if (!tracking) query = query.AsNoTracking();

                var result = await query.FirstOrDefaultAsync(filter);
                if (result != null)
                {
                    // Cache'e geri çağırma fonksiyonu ile yazıyoruz
                    _memoryCache.Set(cacheKey, result, new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                        PostEvictionCallbacks =
                        {
                            new PostEvictionCallbackRegistration
                            {
                                EvictionCallback = (key, value, reason, state) =>
                                {
                                    CacheManager.RemoveCacheKey((string)key);
                                }
                            }
                        }
                    });

                    return new SuccessDataResult<T>(result, ResponseMessage.SuccessMessage);
                }
                else
                {
                    return new ErrorDataResult<T>(ResponseMessage.NotFoundObject);
                }
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<T>(ExceptionHelper.GetErrorMessage(ex));
            }
        }

        // AddAsync metodu - Geri çağırma fonksiyonu ile cache temizleme
        public async Task<IDataResult<T>> AddAsync(T entity)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                EntityEntry<T> state = await Table.AddAsync(entity);
                await SaveAsync();
                stopwatch.Stop();
                Debug.WriteLine($"AddAsync took {stopwatch.ElapsedMilliseconds}ms");

                // Cache'i temizle (Invalidate)
                CacheManager.ClearCacheForType<T>(_memoryCache);

                return new SuccessDataResult<T>(entity, ResponseMessage.SuccessMessage);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<T>(ExceptionHelper.GetErrorMessage(ex));
            }
        }

        // UpdateAsync metodu - Geri çağırma fonksiyonu ile cache temizleme
        public async Task<IResult> UpdateAsync(T entity)
        {
            try
            {
                var entry = _db.Entry(entity);
                if (entry.State == EntityState.Detached)
                {
                    Table.Attach(entity);
                }
                entry.State = EntityState.Modified;
                await SaveAsync();

                // Cache'i temizle (Invalidate)
                CacheManager.ClearCacheForType<T>(_memoryCache);

                return new SuccessDataResult<T>(entity, ResponseMessage.SuccessMessage);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<T>(ExceptionHelper.GetErrorMessage(ex));
            }
        }

        // Delete metodu - Geri çağırma fonksiyonu ile cache temizleme
        public async Task<IResult> Delete(T entity)
        {
            try
            {
                Table.Remove(entity);
                await SaveAsync();

                // Cache'i temizle (Invalidate)
                CacheManager.ClearCacheForType<T>(_memoryCache);

                return new SuccessDataResult<T>(entity, ResponseMessage.SuccessMessage);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<T>(ExceptionHelper.GetErrorMessage(ex));
            }
        }

        public async Task<IDataResult<int>> CountAsync(Expression<Func<T, bool>>? filter = null, bool tracking = false)
        {
            string filterString = filter != null ? ExpressionNormalizer.NormalizeExpression(filter) : "NoFilter";
            string filterHash = HashingHelper.GetSha256Hash(filterString); // Filtreyi hash'liyoruz
            var cacheKey = $"{typeof(T).Name}_count_{filterHash}";

            // Cache key'i listeye ekliyoruz
            CacheManager.AddCacheKey(cacheKey);

            if (_memoryCache.TryGetValue(cacheKey, out int cachedCount))
            {
                return new SuccessDataResult<int>(cachedCount);
            }

            try
            {
                var query = Table.AsQueryable();
                if (!tracking) query = query.AsNoTracking();

                int count = filter != null ? await query.CountAsync(filter) : await query.CountAsync();

                // Cache'e geri çağırma fonksiyonu ile yazıyoruz
                _memoryCache.Set(cacheKey, count, new MemoryCacheEntryOptions()
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                    PostEvictionCallbacks =
            {
                new PostEvictionCallbackRegistration
                {
                    EvictionCallback = (key, value, reason, state) =>
                    {
                        CacheManager.RemoveCacheKey((string)key);
                    }
                }
            }
                });

                return new SuccessDataResult<int>(count, ResponseMessage.SuccessMessage);
            }
            catch (Exception ex)
            {
                return new ErrorDataResult<int>(ExceptionHelper.GetErrorMessage(ex));
            }
        }


        private async Task<int> SaveAsync()
        {
            return await _db.SaveChangesAsync();
        }
    }
}
