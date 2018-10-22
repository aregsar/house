using System;
using Microsoft.AspNetCore.Identity;

namespace house.Data
{
    public class AppUser : IdentityUser<int>
    {
        public string FullName { get; set; }

        public AppUser()
        {
        }
    }
}
