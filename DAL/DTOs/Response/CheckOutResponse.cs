using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class CheckOutResponse : BaseResponse
    {
        public string? Url { get; set; }
        public string? PaymentId
        {
            get; set;

        }
    }
}
