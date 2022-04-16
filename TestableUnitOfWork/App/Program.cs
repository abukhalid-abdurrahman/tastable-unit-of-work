using System;
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
            InvokeDapperUnitOfWorkExample();
        }

        private static void InvokeEfUnitOfWorkExample()
        {
            Console.WriteLine("Creating payment context (db context)...");
            using PaymentContext paymentContext = new PaymentContext();
            using IUnitOfWork unitOfWork = new EfUnitOfWork(paymentContext);
            using IPaymentRepository paymentRepository = new EfPaymentRepository(paymentContext, unitOfWork.Transaction);
            Console.WriteLine("Db created successfully, creating service instance...");
            using IPaymentService paymentService = new PaymentService.PaymentService(paymentRepository, unitOfWork);
            
            Console.WriteLine("Performing credit payment...");
            paymentService.CreditPayment(1000, "4444 0000 9999 1111");
            Console.WriteLine("Performing credit payment completed successfully...");
        }
        
        private static void InvokeDapperUnitOfWorkExample()
        {
            var connectionBuilder = new NpgsqlConnectionStringBuilder
            {
                Database = "payments",
                Host = "localhost",
                Username = "postgres",
                Password = "faridun",
                Timezone = "Asia/Dushanbe"
            };
            
            Console.WriteLine("Creating payment context (db context)...");
            using IUnitOfWork unitOfWork = new DapperUnitOfWork(new NpgsqlConnection()
            {
                ConnectionString = connectionBuilder.ConnectionString
            });
            using IPaymentRepository paymentRepository = new DapperPaymentRepository(unitOfWork.Connection, unitOfWork.Transaction);
            Console.WriteLine("Db created successfully, creating service instance...");
            using IPaymentService paymentService = new PaymentService.PaymentService(paymentRepository, unitOfWork);
            
            Console.WriteLine("Performing debit payment...");
            paymentService.DebitPayment(1500, "4444 0000 9999 1111");
            Console.WriteLine("Performing debit payment completed successfully...");

        }
    }
}