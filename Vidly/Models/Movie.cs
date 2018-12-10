using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Movie
    {

        private DataContext DataContext;

        public Movie()
        {
        }

        public Movie(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

		public int Id { get; set; }
        
        [Required]
		public string Name { get; set; }

		[Required]
		public int  GenreId { get; set; }

		[Required]
		public DateTime  ReleaseDate { get; set; }

       
		public DateTime? DateAdded { get; set; }

		[Required]
		public int NumberInStock { get; set; }
        public int NumberAvailable{get;set;}
        
		public Genre Genre { get; set; }

	}
}
