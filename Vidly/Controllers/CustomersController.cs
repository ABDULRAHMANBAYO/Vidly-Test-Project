using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Microsoft.EntityFrameworkCore;
using Vidly.ViewModels;
using Microsoft.AspNetCore.Authorization;



// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers
{
    [RequireHttps]
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
        
        [Authorize(Roles=RoleName.Admin)]
		public ActionResult New()
		{
			var membershipTypes = _context.MembershipType.ToList();
			var viewModel = new CustomerFormViewModel
			{
				Customer = new Customer(),
				MembershipType = membershipTypes
			};

			return View("CustomerForm",viewModel); 
		} 

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles=RoleName.Admin)]
		public ActionResult Save(Customer customer) //Model Binding
		{
			if (!ModelState.IsValid)
			{
				var viewModel = new CustomerFormViewModel
				{
					Customer = customer,
					MembershipType = _context.MembershipType.ToList()
				};
				return View("CustomerForm", viewModel);
			}

            if(customer.Id == 0)             				_context.customers.Add(customer);              else                  { 				var customerInDb = _context.customers.Single(c => c.Id == customer.Id);              customerInDb.Name = customer.Name;              customerInDb.DateOfBirth = customer.DateOfBirth;              customerInDb.MembershipTypeId = customer.MembershipTypeId;              customerInDb.IsSubscribedToNewsletter= customer.IsSubscribedToNewsletter;                  }           _context.SaveChanges();              
			return RedirectToAction("Index","Customers");
		}

		// GET: /<controller>/
        //[Authorize] 
		public IActionResult Index()
		{  
			if(User.IsInRole(RoleName.Admin))
				return View("Index");
			return View("ViewOnlyIndex");
		}
		public IActionResult Details(int id)
		{

			var customer = _context.customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
			//var customer = _context.customers.Include(c => c.DateOfBirth).SingleOrDefault(c => c.Id == id);

            return View(customer);
			//return View(customer1);
               
		}
		[Authorize(Roles=RoleName.Admin)]
		public IActionResult Edit(int id)
		{
			var customer = _context.customers.SingleOrDefault(c => c.Id == id);


			var viewModel = new CustomerFormViewModel
			{
				Customer = customer,
				MembershipType = _context.MembershipType.ToList()
			};
			return View("CustomerForm", viewModel);
		}








	}
}

