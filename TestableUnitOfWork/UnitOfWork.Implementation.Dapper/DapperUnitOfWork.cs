using System;
using System.Data;
using UnitOfWork.Interfaces;

namespace UnitOfWork.Implementation.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _dbConnection;  
        private IDbTransaction _dbTransaction;

        private bool _disposed = false;
        
        public DapperUnitOfWork(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public IDbConnection Connection => _dbConnection;
        public IDbTransaction Transaction => _dbTransaction;
        
        public void Begin()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperUnitOfWork));
            if(_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            _dbTransaction = _dbConnection.BeginTransaction();
        }

        public void Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperUnitOfWork));
            if(_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            _dbTransaction.Commit();
        }

        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperUnitOfWork));
            if(_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
            _dbTransaction.Rollback();
        }
        
        public void Dispose()
        {
            if(_disposed) return;
            
            _dbTransaction?.Dispose();
            _dbConnection?.Dispose();

            _disposed = true;
        }
    }
}