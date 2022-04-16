using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace UnitOfWork.Implementation.EF
{
    public sealed class PaymentContext : DbContext
    {
        public DbSet<Payment> Payments { get; set; }
        
        public string ConnectionString
        {
            get
            {
                var connectionBuilder = new NpgsqlConnectionStringBuilder
                {
                    Database = "payments",
                    Host = "localhost",
                    Username = "postgres",
                    Password = "faridun",
                    Timezone = "Asia/Dushanbe"
                };
                return connectionBuilder.ConnectionString;
            }
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}