using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Entities.Enums;
using Entities.Models;
using Npgsql;
using Repositories;
using Repositories.Implementation.Dapper;
using Repositories.Implementation.EF;
using UnitOfWork.Implementation.Dapper;
using UnitOfWork.Implementation.EF;
using UnitOfWork.Interfaces;

namespace Banchmarks
{
    [MemoryDiagnoser]
    public class DapperVsEfBenchmarking
    {
        private readonly string _connectionString = "Server=localhost;Port=5432;User Id=postgres;Password=faridun;Database=payments";
        
        [Benchmark]
        public int InsertViaDapper()
        {
            using IUnitOfWork unitOfWork = new DapperUnitOfWork(new NpgsqlConnection()
            {
                ConnectionString = _connectionString
            });
            using IPaymentRepository paymentRepository = new DapperPaymentRepository(unitOfWork.Connection, unitOfWork.Transaction);
            var paymentId = paymentRepository.CreatePayment(new Payment()
            {
                Amount = 1000,
                CustomerCard = "4444000099993333",
                DateCreated = DateTimeOffset.Now,
                DateUpdated = DateTimeOffset.Now,
                PaymentStatus = PaymentStatus.Approved,
                PaymentType = PaymentType.Debit
            });
            return paymentId;
        }
        
        [Benchmark]
        public int InsertViaEf()
        {
            using PaymentContext paymentContext = new PaymentContext(_connectionString);
            using IUnitOfWork unitOfWork = new EfUnitOfWork(paymentContext);
            using IPaymentRepository paymentRepository = new EfPaymentRepository(paymentContext, unitOfWork.Transaction);
            var paymentId = paymentRepository.CreatePayment(new Payment()
            {
                Amount = 1000,
                CustomerCard = "4444000099993333",
                DateCreated = DateTimeOffset.Now,
                DateUpdated = DateTimeOffset.Now,
                PaymentStatus = PaymentStatus.Approved,
                PaymentType = PaymentType.Debit
            });
            return paymentId;
        }

        [Benchmark]
        public Payment InsertAndGetPaymentViaDapper()
        {
            using IUnitOfWork unitOfWork = new DapperUnitOfWork(new NpgsqlConnection()
            {
                ConnectionString = _connectionString
            });
            using IPaymentRepository paymentRepository = new DapperPaymentRepository(unitOfWork.Connection, unitOfWork.Transaction);
            var paymentId = paymentRepository.CreatePayment(new Payment()
            {
                Amount = 1000,
                CustomerCard = "4444000099993333",
                DateCreated = DateTimeOffset.Now,
                DateUpdated = DateTimeOffset.Now,
                PaymentStatus = PaymentStatus.Approved,
                PaymentType = PaymentType.Debit
            });
            return paymentRepository.GetPayment(paymentId);
        }

        [Benchmark]
        public Payment InsertAndGetPaymentViaEf()
        {
            using PaymentContext paymentContext = new PaymentContext(_connectionString);
            using IUnitOfWork unitOfWork = new EfUnitOfWork(paymentContext);
            using IPaymentRepository paymentRepository = new EfPaymentRepository(paymentContext, unitOfWork.Transaction);
            var paymentId = paymentRepository.CreatePayment(new Payment()
            {
                Amount = 1000,
                CustomerCard = "4444000099993333",
                DateCreated = DateTimeOffset.Now,
                DateUpdated = DateTimeOffset.Now,
                PaymentStatus = PaymentStatus.Approved,
                PaymentType = PaymentType.Debit
            });
            return paymentRepository.GetPayment(paymentId);
        }
    }
    
    internal static class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }
    }
}