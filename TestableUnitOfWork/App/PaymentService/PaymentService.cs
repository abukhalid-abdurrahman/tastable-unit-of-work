using System;
using App.Repository;
using UnitOfWork.Interfaces;

namespace App.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }
        
        public void DebitPayment(decimal amount, string customerCard)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void CreditPayment(decimal amount, string customerCard)
        {
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}