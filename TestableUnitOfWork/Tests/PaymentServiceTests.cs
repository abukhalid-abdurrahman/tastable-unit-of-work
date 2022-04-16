using System.Collections.Generic;
using Entities.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using Repositories;
using Services.PaymentService;
using UnitOfWork.Interfaces;

namespace Tests
{
    [TestFixture]
    public class PaymentServiceTests
    {
        private IConfiguration _configuration;

        [SetUp]
        public void SetUp()
        {
            var dbConfig = new Dictionary<string, string>
            {
                { "DbConnectionString", "Server=localhost;Port=5432;User Id=postgres;Password=faridun;Database=payments" }
            };

            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(dbConfig)
                .Build();
        }
        
        [Test]
        [TestCase(1200, "4444000011119999")]
        [TestCase(2000, "5808000077776969")]
        [TestCase(1190, "5808000078876969")]
        public void PaymentService_DebitPayment_ShouldSuccessfullyCreateDebitPayment_Scenario(decimal amount, string customerCard)
        {
            // Setting up unit of work methods
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(x => x.Begin()).Callback(() => { });
            unitOfWork.Setup(x => x.Commit()).Callback(() => { });
            unitOfWork.Setup(x => x.Rollback()).Callback(() => { });
            
            // Setting up repositories method
            var paymentRepository = new Mock<IPaymentRepository>();
            paymentRepository.Setup(x => x.CreatePayment(It.IsAny<Payment>())).Returns(10);
            paymentRepository.Setup(x => x.UpdatePayment(It.IsAny<Payment>())).Callback(() => { });
            
            using IPaymentService paymentService = new PaymentService(paymentRepository.Object, unitOfWork.Object);
            paymentService.DebitPayment(amount, customerCard);
            
            // Sorry, nothing to assert from PaymentService...
            Assert.Pass();
        }
    }
}