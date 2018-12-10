using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
		private readonly DataContext _context;//Inquire

        public CustomersController(DataContext ctx)
        {
            _context = ctx;

            }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        // GET: api/values
        [HttpGet]
		public IEnumerable<Customer> GetCustomer()
        {
           var customer =_context.customers.Include(c =>c.MembershipType).ToList();
            return (customer);
        }

        // GET api/values/5
        [HttpGet("{id}",Name="Customer")]
        public IActionResult GetCustomer(int id)
        {
			var customer = _context.customers.SingleOrDefault(c => c.Id == id);

			if (customer == null)
            {
                return NotFound();
            }
                
            return Ok(customer);
        }

        // POST api/values
        [HttpPost]
        [Authorize(Roles=RoleName.Admin)]
        public IActionResult Create([FromBody]Customer customer)
        {
			if (!ModelState.IsValid)
            {
                return BadRequest();
            }
				
			_context.customers.Add(customer);
			_context.SaveChanges();

            return CreatedAtRoute("Customer", new { id = customer.Id }, customer);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [Authorize(Roles=RoleName.Admin)]
        public IActionResult UpdateCustomer(int id,[FromBody] Customer customer)
        {
			if (!ModelState.IsValid)
            {
                return NotFound();
            }

				var customerInDb = _context.customers.SingleOrDefault(c => c.Id == id);

			if (customerInDb == null)
            {
                return BadRequest();
            }
			
			customerInDb.Name = customer.Name;
            customerInDb.DateOfBirth = customer.DateOfBirth;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
        
			_context.SaveChanges();

            return NoContent();
            
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [Authorize(Roles=RoleName.Admin)]
        public IActionResult DeleteCustomer(int id)
        {
		var customerInDb = _context.customers.SingleOrDefault(c => c.Id == id);

		if (customerInDb == null)
            {
                return NotFound();
            }
			_context.customers.Remove(customerInDb);
		_context.SaveChanges();
            return NoContent();
        }
    }
}
