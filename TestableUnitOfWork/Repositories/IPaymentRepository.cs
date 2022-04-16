using System;
using Entities.Models;

namespace Repositories
{
    public interface IPaymentRepository : IDisposable
    {
        int CreatePayment(Payment payment);
        void UpdatePayment(Payment payment);
        Payment GetPayment(int id);
    }
}