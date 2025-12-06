using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class RegisterResponse
    {
        public bool Success { get; set; }
        public string Message { set; get; }
        public List<string>?error { get; set; }
    }
}
