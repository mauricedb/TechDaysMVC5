using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TechDaysMVC5.Models;

namespace TechDaysMVC5.Api
{
    public class TestableController : ApiController
    {
        private ICustomerRepository _repo;

        public TestableController()
            : this(new CustomerRepository())
        {
        }

        public TestableController(ICustomerRepository repo)
        {
            _repo = repo;
        }

        // GET api/Customers
        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _repo.GetAllAsync();
        }

        // GET api/Customers/5
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> GetCustomer(string id)
        {
            Customer customer = await _repo.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // PUT api/Customers/5
        public async Task<IHttpActionResult> PutCustomer(string id, Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customer.CustomerID)
            {
                return BadRequest();
            }

            _repo.Update(customer);

            await _repo.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Customers
        [ResponseType(typeof(Customer))]
        public async Task<IHttpActionResult> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repo.Add(customer);

            await _repo.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customer.CustomerID }, customer);
        }
    }
}