using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EMag.Models.Sales
{
    public class SalesModel
    {
        public int ID { get; set; }
        public string CustomerFullName { get; set; }
        public string ProductName { get; set; }
        [DataType(DataType.Currency)]
        [Range(0, 99999)]
        public double Price { get; set; }
        [Range(0, 99999)]
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
    }
}