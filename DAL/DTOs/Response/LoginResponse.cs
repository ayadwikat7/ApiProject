using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class LoginResponse
    {
        public string Message { set; get; }
        public bool Success { get; set; }
        public List<string>? error { get; set; }
        public string ?AccessToken { get; set; }
    }
}
