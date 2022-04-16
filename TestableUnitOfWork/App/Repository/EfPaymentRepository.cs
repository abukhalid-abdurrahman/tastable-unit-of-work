﻿using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using UnitOfWork.Implementation.EF;

namespace App.Repository
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
        
        public void CreatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            _paymentContext.Database.UseTransaction(_dbTransaction as DbTransaction);
            _paymentContext.Payments.Add(payment);
            _paymentContext.SaveChanges();
        }

        public void UpdatePayment(Payment payment)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            _paymentContext.Database.UseTransaction(_dbTransaction as DbTransaction);
            _paymentContext.Payments.Remove(payment);
            _paymentContext.SaveChanges();
        }

        public Payment GetPayment(int id)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(DapperPaymentRepository));
            _paymentContext.Database.UseTransaction(_dbTransaction as DbTransaction);
            return _paymentContext.Payments.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        private void ReleaseUnmanagedResources()
        {
            if(_disposed) return;
            
            _dbTransaction.Dispose();
            _paymentContext.Dispose();

            _disposed = true;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~EfPaymentRepository()
        {
            ReleaseUnmanagedResources();
        }
    }
}