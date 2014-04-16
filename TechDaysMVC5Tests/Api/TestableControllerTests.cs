using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechDaysMVC5.Api;
using TechDaysMVC5.Models;

namespace TechDaysMVC5Tests.Api
{
    [TestClass]
    public class TestableControllerTests
    {
        [TestMethod]
        public async Task ShouldReturnAListOfCustomers()
        {
            // Arrange
            var repo = new DummyCustomerRepository();
            var controller = new TestableController(repo);

            // Act
            var customers = await controller.GetCustomers();

            // Assert
            Assert.AreEqual(2, customers.Count());
        }

        [TestMethod]
        public async Task ShouldReturnACustomer()
        {
            // Arrange
            var repo = new DummyCustomerRepository();
            var controller = new TestableController(repo);

            // Act
            var result = await controller.GetCustomer("1") as OkNegotiatedContentResult<Customer>;

            // Assert
            Assert.IsNotNull(result);
            var customer = result.Content;
            Assert.IsNotNull(customer);
            Assert.AreEqual("1", customer.CustomerID);
            Assert.AreEqual("Company 1", customer.CompanyName);
        }

        [TestMethod]
        public async Task ShouldNotReturnACustomer()
        {
            // Arrange
            var repo = new DummyCustomerRepository();
            var controller = new TestableController(repo);

            // Act
            var result = await controller.GetCustomer("NONE");

            // Assert
            Assert.IsInstanceOfType(result, typeof (NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldAllowUpdatingACustomer()
        {
            // Arrange
            var repo = new DummyCustomerRepository();
            var controller = new TestableController(repo);
            var customer = new Customer {CustomerID = "1"};

            // Act
            var result = await controller.PutCustomer("1", customer) as StatusCodeResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(HttpStatusCode.NoContent, result.StatusCode);
        }

        [TestMethod]
        public async Task ShouldNotAllowUpdatingACustomerAsAnother()
        {
            // Arrange
            var repo = new DummyCustomerRepository();
            var controller = new TestableController(repo);
            var customer = new Customer {CustomerID = "1"};

            // Act
            var result = await controller.PutCustomer("2", customer);

            // Assert
            Assert.IsInstanceOfType(result, typeof (BadRequestResult));
        }


        [TestMethod]
        public async Task ShouldAcceptPostingANewCustomer()
        {
            // Arrange
            var repo = new DummyCustomerRepository();
            var controller = new TestableController(repo);
            var newCustomer = new Customer
            {
                CompanyName = "The new Company"
            };

            // Act  
            var result = await controller.PostCustomer(newCustomer) as CreatedAtRouteNegotiatedContentResult<Customer>;

            // Assert
            Assert.IsNotNull(result);
            var addedCustomer = result.Content;
            Assert.AreEqual("DefaultApi", result.RouteName);
            Assert.AreEqual(addedCustomer.CustomerID, result.RouteValues["Id"]);

            Assert.IsNotNull(addedCustomer.CustomerID);
            Assert.IsNotNull(addedCustomer.CompanyName);
            Assert.IsNull(addedCustomer.ContactName);
        }

        private class DummyCustomerRepository : ICustomerRepository
        {
            private readonly List<Customer> _data = new List<Customer>();

            public DummyCustomerRepository()
            {
                _data.Add(new Customer {CustomerID = "1", CompanyName = "Company 1"});
                _data.Add(new Customer {CustomerID = "2", CompanyName = "Company 2"});
            }

            public Task<IEnumerable<Customer>> GetAllAsync()
            {
                return Task.FromResult(_data.AsEnumerable());
            }

            public Task<Customer> FindAsync(string id)
            {
                return Task.FromResult(_data.FirstOrDefault(c => c.CustomerID == id));
            }

            public Customer Add(Customer customer)
            {
                customer.CustomerID = Environment.TickCount.ToString();
                _data.Add(customer);
                return customer;
            }

            public Task<int> SaveChangesAsync()
            {
                return Task.FromResult(1);
            }


            public void Update(Customer customer)
            {
            }
        }
    }
}