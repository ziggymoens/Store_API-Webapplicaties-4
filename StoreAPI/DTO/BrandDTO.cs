using StoreAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.DTO
{
    public class BrandDTO
    {
        [Required]
        public string Name { get; set; }
        //public IEnumerable<SneakerDTO> Sneakers { get; set; }
    }
}
