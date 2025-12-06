using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Request
{
    public class LoginRequest
    {
        public string Email { set; get; }
        public string Password { set; get; }
    }
}
