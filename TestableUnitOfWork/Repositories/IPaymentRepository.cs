using System;
using Entities.Models;

namespace Repositories
{
    public interface IPaymentRepository : IDisposable
    {
        /// <summary>
        /// Saving payment into database
        /// </summary>
        /// <param name="payment">Payment entity</param>
        /// <returns>Payment Id</returns>
        int CreatePayment(Payment payment);
        /// <summary>
        /// Updating payment record in database
        /// </summary>
        /// <param name="payment">Payment entity</param>
        void UpdatePayment(Payment payment);
        /// <summary>
        /// Finds payment by id, and return it instance
        /// </summary>
        /// <param name="id">Payment Id</param>
        /// <returns>Payment instance</returns>
        Payment GetPayment(int id);
    }
}