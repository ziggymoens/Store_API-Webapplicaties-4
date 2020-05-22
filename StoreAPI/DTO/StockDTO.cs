using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.DTO
{
    public class StockDTO
    {
        [Required]
        public int Amount { get; set; }
        [Required]
        public double Size { get; set; }
    }
}
