using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.DTO
{
    public class SneakerDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public BrandDTO Brand { get; set; }
        [Required]
        public string ReleaseDate { get; set; }
        [Required]
        public string Barcode { get; set; }

        //[Required]
        public IEnumerable<StockDTO> Stock { get; set; }
    }
}
