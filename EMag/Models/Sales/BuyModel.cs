using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMag.Models.Sales
{
    public class BuyModel
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        [Range (0,999)]
        public int Quantity { get; set; }
    }
}