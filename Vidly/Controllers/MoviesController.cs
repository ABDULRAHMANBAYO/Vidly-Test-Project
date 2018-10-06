using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;
using Microsoft.EntityFrameworkCore;
using Vidly.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
		private readonly DataContext _context;//Inquire

		public MoviesController(DataContext ctx)
        {
            _context = ctx;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
		public ActionResult New()
        {
			var genres = _context.Genre.ToList();

            var viewModel = new MovieFormViewModel
            {
				Movie = new Movie(),//Creates a new instance of class movie
				Genre= genres
            };

            return View("MovieForm", viewModel);
            /*var genreTypes = _context.GenreType.ToList();
			var viewModel = new MovieFormViewModel
			{
				Movie = new Movie(),
				GenreType = genreTypes
            };

            return View("MovieForm", viewModel);*/
        }

        [HttpPost]
		public ActionResult Save(Movie movie) //Model Binding
        {
			if(!ModelState.IsValid)
			{
				var viewModel = new MovieFormViewModel
				{
					Movie = movie,
					Genre = _context.Genre.ToList()
				};
				return View("MovieForm", viewModel);
			}


			if (movie.Id == 0)
			{
				movie.DateAdded= DateTime.Now;
				_context.movies.Add(movie);
			}
				
            else
            {
				var movieInDb = _context.movies.Single(c => c.Id == movie.Id);


                //TryUpdateModel(customerInDb);
                //_context.customers.Update(customer);
				movieInDb.Name = movie.Name;
				movieInDb.ReleaseDate= movie.ReleaseDate;
				movieInDb.GenreId = movie.GenreId;
				movieInDb.NumberInStock = movie.NumberInStock;
            }
            _context.SaveChanges();


            return RedirectToAction("Index", "Movies");
        }


        // GET: /<controller>/

        public IActionResult Index()
        {
			

            
			return View();
        }
		public IActionResult Details(int id)
        {

			var movie = _context.movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);
          
			return View(movie);
      

        }

		public IActionResult Edit(int id)
        {
			var movie = _context.movies.SingleOrDefault(c => c.Id == id);


			var viewModel = new MovieFormViewModel
            {
				Movie = movie,
				Genre = _context.Genre.ToList()
            };
            return View("MovieForm", viewModel);
        }



    }
}
