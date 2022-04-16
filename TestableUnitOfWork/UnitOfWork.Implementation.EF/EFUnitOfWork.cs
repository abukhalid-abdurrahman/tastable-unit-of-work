using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UnitOfWork.Interfaces;

namespace UnitOfWork.Implementation.EF
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly PaymentContext _paymentDbContext;
        private readonly IDbConnection _dbConnection;  
        private IDbTransaction _dbTransaction;

        private bool _disposed = false;
        
        public EfUnitOfWork(PaymentContext paymentDbContext)
        {
            _paymentDbContext = paymentDbContext;
            _dbConnection = _paymentDbContext.Database.GetDbConnection();
        }

        public IDbConnection Connection => _dbConnection;
        public IDbTransaction Transaction => _dbTransaction;
        
        public void Begin()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
            _dbTransaction = _paymentDbContext.Database.BeginTransaction().GetDbTransaction();
        }

        public void Commit()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
            _dbTransaction.Commit();
        }

        public void Rollback()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfUnitOfWork));
            _dbTransaction.Rollback();
            
        }

        public void Dispose()
        {
            if(_disposed) return;

            _dbConnection?.Dispose();
            _dbTransaction?.Dispose();
            _paymentDbContext?.Dispose();
            
            _disposed = true;
        }
    }
}