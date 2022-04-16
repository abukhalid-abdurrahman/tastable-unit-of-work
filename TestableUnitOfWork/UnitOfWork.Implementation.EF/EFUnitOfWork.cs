using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Interfaces;

namespace UnitOfWork.Implementation.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IDbConnection _connection;  
        private IDbTransaction _transaction;

        private bool _disposed = false;
        public EfUnitOfWork(DbContext context)
        {
            _context = context;
            _connection = _context.Database.GetDbConnection();
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;
        
        public void Begin()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
            _transaction = _context.Database.BeginTransaction().GetDbTransaction();
        }

        public void Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
            _transaction.Rollback();
            
        }
        
        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }
        
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Connection?.Dispose();
                Transaction?.Dispose();
                _context.Dispose();
            }
            ReleaseUnmanagedResources();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~EfUnitOfWork()
        {
            Dispose(false);
        }
    }
}