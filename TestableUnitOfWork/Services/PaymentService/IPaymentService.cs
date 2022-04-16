using System;

namespace Services.PaymentService
{
    public interface IPaymentService : IDisposable
    {
        /// <summary>
        /// Debit specified amount of money from customer card
        /// </summary>
        /// <param name="amount">Debit amount</param>
        /// <param name="customerCard">Customer card (pan)</param>
        void DebitPayment(decimal amount, string customerCard);
        /// <summary>
        /// Credit specified card with specified amount of money
        /// </summary>
        /// <param name="amount">Credit amount</param>
        /// <param name="customerCard">Customer card (pan)</param>
        void CreditPayment(decimal amount, string customerCard);
    }
}