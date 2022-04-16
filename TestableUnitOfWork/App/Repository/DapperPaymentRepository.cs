using System;
using System.Data;
using Dapper;
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

        public int CreatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));

            const string query = "INSERT INTO public.\"Payments\"(\"PaymentStatus\", \"PaymentType\", \"CustomerCard\", \"Amount\", \"DateCreated\", \"DateUpdated\") VALUES (@PaymentStatus, @PaymentType, @CustomerCard, @Amount, @DateCreated, @DateUpdated) RETURNING \"Id\";";
            return _connection.QueryFirstOrDefault<int>(query, payment, _dbTransaction);
        }

        public void UpdatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            const string query = "UPDATE \"Payments\" SET \"PaymentStatus\"=@PaymentStatus, \"DateUpdated\"=@DateUpdated WHERE \"Id\" = @Id;";
            _connection.ExecuteScalar(query, payment, _dbTransaction);
        }

        public Payment GetPayment(int id)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            const string query = "SELECT * FROM \"Payments\" WHERE \"Id\" = @Id;";
            return _connection.QueryFirstOrDefault<Payment>(query, new { Id = id }, _dbTransaction);
        }

        private void ReleaseUnmanagedResources()
        {
            if (_disposed) return;
            
            _dbTransaction?.Dispose();
            _connection?.Dispose();

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