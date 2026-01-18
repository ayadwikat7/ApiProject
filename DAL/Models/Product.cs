using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product : baseModel
    {
        public int Id { get; set; }
        public string MainImage { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public List <ProductTransulation> ProductTransulations { get; set; }

        public List<ProductsImage> SubImages { get; set; }
    }
}
