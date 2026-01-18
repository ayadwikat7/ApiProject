using DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class ProductsResponse
    {
        public int Id { get; set; }
        public string CreatedBy { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public statuse Statuse { get; set; }

        public string MainImage { get; set; }
        public List<string> SubImages { get; set; }

        public List<ProductsTransulationResponse> ProductsTransulations { get; set; }
    }
}
