using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class ErrorDetalies
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } 
        public string stackTrace { get; set; }
    }
}
