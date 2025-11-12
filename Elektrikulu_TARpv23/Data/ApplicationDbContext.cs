using Elektrikulu_TARpv23.Models;
using Microsoft.EntityFrameworkCore;

namespace Elektrikulu_TARpv23.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Consumer> Customers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PaymentStatus> PaymentStatuss { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
