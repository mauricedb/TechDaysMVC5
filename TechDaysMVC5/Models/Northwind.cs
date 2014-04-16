using System.Data.Entity;

namespace TechDaysMVC5.Models
{
    public class NorthwindContext: DbContext
    {
        public NorthwindContext()
            : base("NorthwindConnection")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}