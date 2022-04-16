using App.PaymentService;
using App.Repository;
using Npgsql;
using UnitOfWork.Implementation.Dapper;
using UnitOfWork.Implementation.EF;
using UnitOfWork.Interfaces;

namespace App
{
    internal static class Program
    {
        public static void Main()
        {
            InvokeEfUnitOfWorkExample();
        }

        private static void InvokeEfUnitOfWorkExample()
        {
            using PaymentContext paymentContext = new PaymentContext();
            using IUnitOfWork unitOfWork = new EfUnitOfWork(paymentContext);
            using IPaymentRepository paymentRepository = new EfPaymentRepository(paymentContext, unitOfWork.Transaction);
            using IPaymentService paymentService = new PaymentService.PaymentService(paymentRepository, unitOfWork);
            
            paymentService.CreditPayment(1000, "4444 0000 9999 1111");
            paymentService.DebitPayment(1500, "4444 0000 9999 1111");
        }
        
        private static void InvokeDapperUnitOfWorkExample()
        {
            using IUnitOfWork unitOfWork = new DapperUnitOfWork(new NpgsqlConnection()
            {
                ConnectionString = "Host=localhost;Port=5433;Database=payments;Username=postgres;Password=faridun"
            });
            using IPaymentRepository paymentRepository = new DapperPaymentRepository(unitOfWork.Connection, unitOfWork.Transaction);
            using IPaymentService paymentService = new PaymentService.PaymentService(paymentRepository, unitOfWork);
            
            paymentService.CreditPayment(1000, "4444 0000 9999 1111");
            paymentService.DebitPayment(1500, "4444 0000 9999 1111");
        }
    }
}