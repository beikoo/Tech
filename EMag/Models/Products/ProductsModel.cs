using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMag.Models.Products
{
    public class ProductsModel
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(0,99999)]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}