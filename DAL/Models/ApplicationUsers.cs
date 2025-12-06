using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public string City { get; set; }="NotSet";
        public string Street { get; set; }="NotSet";
        public string FullName { get; set; }
    }
}
