using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        private readonly DataContext _context;

        public MoviesController(DataContext ctx)
        {
            _context = ctx;

        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: api/values
        [HttpGet]
        public IEnumerable<Movie> GetMovies()

        {
            var movies = _context.movies.Include(c => c.Genre);
            return movies.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}",Name ="Movie")]
        public IActionResult GetMovie(int id)
        {
            var movie = _context.movies.Include(c=>c.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST api/values
        [HttpPost]
        public IActionResult CreateMovie([FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.movies.Add(movie);
            _context.SaveChanges();

            return CreatedAtRoute("Movie", new { id = movie.Id }, movie);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int id, [FromBody]Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return NotFound();
            }

            var movieInDb = _context.movies.Single(c => c.Id ==id);


            if (movieInDb == null)
            {
                return BadRequest();   
            }
                

            movieInDb.Name = movie.Name;
            movieInDb.ReleaseDate = movie.ReleaseDate;
            movieInDb.GenreId = movie.GenreId;
            movieInDb.NumberInStock = movie.NumberInStock;

            _context.SaveChanges();
            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.movies.Single(c => c.Id ==id);

            if (movieInDb == null)
            {
                return NotFound();
            }
            _context.movies.Remove(movieInDb);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
