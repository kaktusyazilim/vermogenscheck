using Microsoft.EntityFrameworkCore.Storage;
using CorporateWebProject.Application.Repositories;
using CorporateWebProject.Application.UnitOfWork;
using CorporateWebProject.Persistence.Contexs;
using System;
using System.Threading.Tasks;

namespace CorporateWebProject.Persistence.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork, IAsyncDisposable
    {
        private readonly ProjectContext _context;
        private IDbContextTransaction _transaction;

        public UnitOfWork(ProjectContext context)
        {
            _context = context;
        }

        // Transaction başlat
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_transaction == null)
            {
                _transaction = await _context.Database.BeginTransactionAsync();
            }
            return _transaction;
        }

        // Transaction'ı başarıyla tamamla
        public async Task CommitAsync()
        {
            try
            {
                if (_transaction != null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                await DisposeTransactionAsync();
            }
        }

        // Transaction'ı geri al (Rollback)
        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
            await DisposeTransactionAsync();
        }

        // Transaction'ı Dispose et
        private async Task DisposeTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        // Context ve transaction'ı temizle (Dispose)
        public async ValueTask DisposeAsync()
        {
            if (_context != null)
            {
                await _context.DisposeAsync();
            }
            await DisposeTransactionAsync();
        }
    }
}
