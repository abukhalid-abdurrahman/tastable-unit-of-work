using System;
using Entities.Enums;
using Entities.Models;
using Repositories;
using UnitOfWork.Interfaces;

namespace Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        private bool _disposed = false;
        
        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }
        
        public void DebitPayment(decimal amount, string customerCard)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PaymentService));

            if (amount <= 0 || string.IsNullOrEmpty(customerCard))
                throw new ArgumentException();
            
            try
            {
                _unitOfWork.Begin();

                var debitPayment = new Payment()
                {
                    Amount = amount,
                    CustomerCard = customerCard,
                    DateCreated = DateTimeOffset.Now,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentType = PaymentType.Debit
                };
                
                debitPayment.Id = _paymentRepository.CreatePayment(debitPayment);
                
                // TODO send request to payment gateway, to perform debit operation...
                // Updating payment status and update date
                debitPayment.DateUpdated = DateTimeOffset.Now;
                debitPayment.PaymentStatus = PaymentStatus.Approved;
                _paymentRepository.UpdatePayment(debitPayment);

                
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                
                Console.WriteLine(e);
                throw;
            }
        }

        public void CreditPayment(decimal amount, string customerCard)
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(PaymentService));
            
            if (amount <= 0 || string.IsNullOrEmpty(customerCard))
                throw new ArgumentException();
            
            try
            {
                _unitOfWork.Begin();
                
                var creditPayment = new Payment()
                {
                    Amount = amount,
                    CustomerCard = customerCard,
                    DateCreated = DateTimeOffset.Now,
                    PaymentStatus = PaymentStatus.Pending,
                    PaymentType = PaymentType.Credit
                };
                
                _paymentRepository.CreatePayment(creditPayment);
                
                // TODO send request to payment gateway, to perform debit operation...
                // Updating payment status and update date
                creditPayment.DateUpdated = DateTimeOffset.Now;
                creditPayment.PaymentStatus = PaymentStatus.Approved;
                _paymentRepository.UpdatePayment(creditPayment);

                
                _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                _unitOfWork.Rollback();
                
                Console.WriteLine(e);
                throw;
            }
        }

        public void Dispose()
        {
            if(_disposed) return;
            
            _paymentRepository?.Dispose();
            _unitOfWork?.Dispose();

            _disposed = true;
        }
    }
}