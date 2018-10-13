using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;

namespace Vidly.Models
{
    public class ApplicationUser:IdentityUser<Guid>
    {

    }
}
