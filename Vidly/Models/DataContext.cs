using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vidly.Data;
using MySql.Data.MySqlClient;
using Vidly.Models;

namespace Vidly.Models
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
		public DataContext()
		{
		}

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
           
        }


		public DbSet<Customer> customers { get; set; }
		public DbSet<Movie> movies { get; set; }
		public DbSet<MembershipType> MembershipType { get; set; }
		public DbSet<Genre> Genre{ get; set; }
        public DbSet<Rental> Rental{get;set;}

        
        
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
        }

	


    } 
}


