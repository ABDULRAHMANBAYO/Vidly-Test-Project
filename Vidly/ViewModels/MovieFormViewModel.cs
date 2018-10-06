using System;
using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
		
		public IEnumerable<Genre> Genre { get; set; }
		public Movie Movie { get; set; }

    
	}
}
