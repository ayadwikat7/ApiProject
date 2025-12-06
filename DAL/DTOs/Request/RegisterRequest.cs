using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Request
{
    public class RegisterRequest
    {
       public string UserName { set; get; }
        public string Email { set; get; }
        public string Password { set; get; }
        public string FullName { set; get; }
        public string PhoneNumber { set; get; }
    }
}
