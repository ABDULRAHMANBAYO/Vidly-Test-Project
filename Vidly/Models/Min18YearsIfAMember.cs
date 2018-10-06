using System;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.Models
{
	public class Min18YearsIfAMember:ValidationAttribute
    {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			var customer = (Customer)validationContext.ObjectInstance;//Gives access o the containing class

			if (customer.MembershipTypeId==MembershipType.Unknown||customer.MembershipTypeId == MembershipType.PayAsYouGo)
				return ValidationResult.Success;
			if (customer.DateOfBirth == null)
				return new ValidationResult("Birthdate is required");
			var age = DateTime.Today.Year - customer.DateOfBirth.Value.Year;
			return (age >= 18) 
				? ValidationResult.Success 
					                  : new ValidationResult("Customer should be at least 18 years to go on a membership ");

		}
	}
}
