using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DAL.DTOs.Response
{
    public class OrderResponse
    {
            public int Id { get; set; }

            public OrderStatus OrderStatus { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
            public PaiedStatus PaiedStatus { get; set; }

            public decimal AmountPayed { get; set; }

            public string? userName { get; set; }
    }
}
