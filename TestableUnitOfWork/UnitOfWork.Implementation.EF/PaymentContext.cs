using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace UnitOfWork.Implementation.EF
{
    public sealed class PaymentContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        
        public PaymentContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=payments;Username=postgres;Password=faridun");
        }
    }
}