using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class ProductTransulation
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Product product { get; set; }

        public string Language { get; set; }
    }
}
