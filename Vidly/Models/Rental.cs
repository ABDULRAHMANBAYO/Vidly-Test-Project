using System;
using System.Collections.Generic;
namespace Vidly.Models
{
    public class Rental
    {
        public int? Id{get;set;}
        
        public DateTime DateRented{get;set;}
        public DateTime? DateReturned{get;set;}

        public int CustomerId{get;set;}

        public int MovieId{get;set;}

        public Customer Customers{get;set;}

        public Movie Movies{get;set;}

        
        
    }
}