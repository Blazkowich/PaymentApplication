using Microsoft.EntityFrameworkCore;
using Services.Models;

namespace Services.Database
{
    public class PaymentDbContext : DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

        public DbSet<PaymentDetailModel>? PaymentDetails { get; set; }
    }
}
