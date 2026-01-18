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
        public string? PasswordResetCode { get; set; }
        public DateTime? PasswodResretCodeExpirty { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
