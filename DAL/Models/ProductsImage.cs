using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductsImage
    {
        public int Id { get; set; }
        public string ImagName { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
