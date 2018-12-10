using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Models
{
	public class Customer
	{
		private DataContext DataContext;

		public Customer()
		{
		}

		public Customer(DataContext DataContext)
        {
            this.DataContext = DataContext;
        }

        public int  Id { get; set; }
        
        
		[Display(Name = "Date of Birth")] //chaning the label of a form using data annotations
        [Min18YearsIfAMember]
		public  DateTime? DateOfBirth { get; set; }

		[Required(ErrorMessage = "Please enter your name.")]
		[StringLength(155)]
		public string Name { get; set; }

		public bool IsSubscribedToNewsletter { get; set; }

        
        public MembershipType MembershipType { get; set; }

	
		[Display (Name ="Membership Type")]
		public byte MembershipTypeId { get; set; }

        public static implicit operator Customer(bool v)
        {
            throw new NotImplementedException();
        }
    }
}
    

    
