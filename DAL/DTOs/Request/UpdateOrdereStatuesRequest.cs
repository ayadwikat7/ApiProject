using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Request
{
    public class UpdateOrdereStatuesRequest
    {
        public OrderStatus OrderStatus { get; set; }
    }
}
