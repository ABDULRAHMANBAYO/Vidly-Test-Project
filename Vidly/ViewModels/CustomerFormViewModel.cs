using System;
using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
		public IEnumerable<MembershipType> MembershipType { get; set; }
		public Customer Customer { get; set; }
    }
}
