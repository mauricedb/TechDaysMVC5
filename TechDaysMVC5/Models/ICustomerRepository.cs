using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace TechDaysMVC5.Models
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> FindAsync(string id);

        void Update(Customer customer);
        Customer Add(Customer customer);
        Task<int> SaveChangesAsync();
    }

    public class CustomerRepository : ICustomerRepository
    {
        private NorthwindContext db = new NorthwindContext();

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await db.Customers.ToListAsync();
        }


        public async Task<Customer> FindAsync(string id)
        {
            return await db.Customers.FindAsync(id);
        }

        public void Update(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
        }

        public Customer Add(Customer customer)
        {
            customer.CustomerID = Environment.TickCount.ToString();
            db.Customers.Add(customer);
            return customer;
        }

        public Task<int> SaveChangesAsync()
        {
           return db.SaveChangesAsync();
        }
    }

}