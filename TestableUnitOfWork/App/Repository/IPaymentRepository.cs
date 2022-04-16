using Entities.Models;

namespace App.Repository
{
    public interface IPaymentRepository
    {
        void CreatePayment(Payment payment);
        void UpdatePayment(Payment payment);
        Payment GetPayment(int id);
    }
}