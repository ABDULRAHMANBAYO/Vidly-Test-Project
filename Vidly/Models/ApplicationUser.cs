using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        [StringLength(15)]
        public string SocialNumber {get;set;}

        
    }
}
