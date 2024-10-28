using Microsoft.EntityFrameworkCore;
using CorporateWebProject.Application.Wrappers.Abstract;
using CorporateWebProject.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CorporateWebProject.Application.Repositories
{
    public interface IBaseRepository<T> where T : EntityBase
    {
        /// <summary>
        /// Veri tabanındaki ilgili tabloya erişim sağlar.
        /// </summary>
        DbSet<T> Table { get; }

        /// <summary>
        /// Filtre uygulanarak veritabanından veri listesi getirir. Takip varsayılan olarak kapalıdır.
        /// </summary>
        Task<IDataResult<List<T>>> GetListAsync(Expression<Func<T, bool>>? filter = null, bool tracking = false);

        /// <summary>
        /// Filtre uygulanarak tek bir veri getirir. Takip varsayılan olarak kapalıdır.
        /// </summary>
        Task<IDataResult<T>> Get(Expression<Func<T, bool>> filter, bool tracking = false);

        /// <summary>
        /// Yeni bir veri ekler.
        /// </summary>
        Task<IDataResult<T>> AddAsync(T entity);

        /// <summary>
        /// Var olan veriyi günceller.
        /// </summary>
        Task<IResult> UpdateAsync(T entity);

        /// <summary>
        /// Var olan veriyi siler.
        /// </summary>
        Task<IResult> Delete(T entity);

        /// <summary>
        /// Filtre uygulanarak veritabanındaki kayıt sayısını döndürür. Takip varsayılan olarak kapalıdır.
        /// </summary>
        Task<IDataResult<int>> CountAsync(Expression<Func<T, bool>>? filter = null, bool tracking = false);

    }
}
