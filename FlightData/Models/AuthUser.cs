using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace FlightData.Models
{
    public class AuthUser : IdentityUser
    {
        public string Name { get; set; }
    }
}