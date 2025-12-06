using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class responseCategory
    {
        public int Id { get; set; }
        public statuse Statuse { get; set; }
        public List<CategoryTransulationResponse> CategorTransoulations { get; set; }
    }
}
