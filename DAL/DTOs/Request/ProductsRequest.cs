using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Request
{
    public class ProductsRequest
    {
        public List<ProductsTRansulationRequest> ProductsTRansulations { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public int CategoryId { get; set; }

        public IFormFile MainImage { get; set; }
        public List<IFormFile> SubImages { get; set; }
    }
}
