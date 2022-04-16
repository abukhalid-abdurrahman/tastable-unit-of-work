using System;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Repositories;
using Repositories.Implementation.Dapper;
using Repositories.Implementation.EF;
using Services.PaymentService;
using UnitOfWork.Implementation.Dapper;
using UnitOfWork.Implementation.EF;
using UnitOfWork.Interfaces;

namespace App
{
    internal static class Program
    {
        private static readonly IConfigurationRoot Configuration = 
            new ConfigurationBuilder()
                .AddIniFile("config.ini")
                .Build();
        
        public static void Main()
        {
            MakeCreditPayment();
            MakeDebitPayment();
        }

        /// <summary>
        /// Creating credit payment and save it into db,
        /// with ef unit of work and repository implementation
        /// </summary>
        private static void MakeCreditPayment()
        {
            Console.WriteLine("Creating database...");
            using PaymentContext paymentContext = new PaymentContext(Configuration["connectionString"]);
            using IUnitOfWork unitOfWork = new EfUnitOfWork(paymentContext);
            using IPaymentRepository paymentRepository = new EfPaymentRepository(paymentContext, unitOfWork.Transaction);
            Console.WriteLine("Creating database completed successfully, creating service instance...");
            using IPaymentService paymentService = new PaymentService(paymentRepository, unitOfWork);
            
            Console.WriteLine("Performing credit payment...");
            paymentService.CreditPayment(1000, "4444 0000 9999 1111");
            Console.WriteLine("Performing credit payment completed successfully...");
        }
        
        /// <summary>
        /// Creating debit payment and save it into db,
        /// with dapper unit of work and repository implementation
        /// </summary>
        private static void MakeDebitPayment()
        {
            Console.WriteLine("Creating database...");
            using IUnitOfWork unitOfWork = new DapperUnitOfWork(new NpgsqlConnection()
            {
                ConnectionString = Configuration["connectionString"]
            });
            using IPaymentRepository paymentRepository = new DapperPaymentRepository(unitOfWork.Connection, unitOfWork.Transaction);
            Console.WriteLine("Creating database completed successfully, creating service instance...");
            using IPaymentService paymentService = new PaymentService(paymentRepository, unitOfWork);
            
            Console.WriteLine("Performing debit payment...");
            paymentService.DebitPayment(1500, "4444 0000 9999 1111");
            Console.WriteLine("Performing debit payment completed successfully...");
        }
    }
}