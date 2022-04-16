namespace App.PaymentService
{
    public interface IPaymentService
    {
        void DebitPayment(decimal amount, string customerCard);
        void CreditPayment(decimal amount, string customerCard);
    }
}