using System;
using System.Data;
using UnitOfWork.Interfaces;

namespace UnitOfWork.Implementation.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;  
        private IDbTransaction _transaction;

        private bool _disposed = false;
        public DapperUnitOfWork(IDbConnection connection)
        {
            _connection = connection;
        }

        public IDbConnection Connection => _connection;
        public IDbTransaction Transaction => _transaction;
        
        public void Begin()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperUnitOfWork));
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperUnitOfWork));
            _transaction.Commit();
        }

        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperUnitOfWork));
            _transaction.Rollback();
        }

        private void ReleaseUnmanagedResources()
        {
            // TODO release unmanaged resources here
        }

        private void Dispose(bool disposing)
        {
            if(_disposed) return;
            
            if (disposing)
            {
                Connection?.Dispose();
                Transaction?.Dispose();
            }
            ReleaseUnmanagedResources();

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DapperUnitOfWork()
        {
            Dispose(false);
        }
    }
}