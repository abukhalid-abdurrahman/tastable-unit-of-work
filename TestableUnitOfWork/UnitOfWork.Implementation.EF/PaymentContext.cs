using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Implementation.EF
{
    public sealed class PaymentContext : DbContext
    {
        private readonly string _connectionString;
        public DbSet<Payment> Payments { get; set; }

        public PaymentContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}