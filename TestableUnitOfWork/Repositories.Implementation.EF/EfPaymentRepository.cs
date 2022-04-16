using System;
using System.Data;
using System.Linq;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Implementation.EF;

namespace Repositories.Implementation.EF
{
    public class EfPaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _paymentContext;
        private readonly IDbTransaction _dbTransaction;
        
        private bool _disposed = false;

        public EfPaymentRepository(PaymentContext paymentContext, IDbTransaction dbTransaction)
        {
            _paymentContext = paymentContext;
            _dbTransaction = dbTransaction;
        }
        
        public int CreatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfPaymentRepository));
            _paymentContext.Payments.Add(payment);
            _paymentContext.SaveChanges();
            return payment.Id;
        }

        public void UpdatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfPaymentRepository));
            _paymentContext.Payments.Update(payment);
            _paymentContext.SaveChanges();
        }

        public Payment GetPayment(int id)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(EfPaymentRepository));
            return _paymentContext.Payments.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        public void Dispose()
        {
            if(_disposed) return;
            
            _dbTransaction?.Dispose();
            _paymentContext?.Dispose();

            _disposed = true;
        }
    }
}