using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class baseModel
    {
        public int Id { get; set; }

        public statuse status { get; set; } = statuse.Active;
        public DateTime createCateg { get; set; }
        public string CreatedBy { get; set; }// session 12 part1
        public DateTime? UpdatedAt { get; set; }// session 12 part1
        public string? UpdatedBy { get; set; }// session 12 part1
        public DateTime? CreatedAt { get; set; }// session 12 part1

    }
}
