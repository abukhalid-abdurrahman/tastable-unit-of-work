using System;
using System.Data;
using DapperExtensions;
using Entities.Models;

namespace App.Repository
{
    public class DapperPaymentRepository : IPaymentRepository
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _dbTransaction;

        private bool _disposed = false;

        public DapperPaymentRepository(IDbConnection connection, IDbTransaction dbTransaction)
        {
            _connection = connection;
            _dbTransaction = dbTransaction;
        }

        public void CreatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            _connection.Insert(payment, _dbTransaction);
        }

        public void UpdatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            _connection.Update(payment, _dbTransaction);
        }

        public Payment GetPayment(int id)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            return _connection.Get<Payment>(id, _dbTransaction);
        }

        private void ReleaseUnmanagedResources()
        {
            if (_disposed) return;
            
            _dbTransaction.Dispose();
            _connection.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~DapperPaymentRepository()
        {
            ReleaseUnmanagedResources();
        }
    }
}