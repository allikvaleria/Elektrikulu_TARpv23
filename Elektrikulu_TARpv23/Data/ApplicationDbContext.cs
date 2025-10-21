using Elektrikulu_TARpv23.Models;
using Microsoft.EntityFrameworkCore;

namespace Elektrikulu_TARpv23.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Usage> Usages { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
