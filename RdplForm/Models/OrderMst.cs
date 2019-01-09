using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RdplForm.Models
{
    public class OrderMst
    {
        [Required]
        [Range(0, 9999, ErrorMessage = "Code not be negative ")]
        public int No { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
          [Required]
        public string CustomerName { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
    }
    public class OrderDetail
    {
        [Required]
        public string Item { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "Quantity not be negative ")]
        public int Quantity { get; set; }
        [Required]
        [Range(0, 999999999, ErrorMessage = "Rate not be negative ")]
        public int Rate { get; set; }

    }
    
}