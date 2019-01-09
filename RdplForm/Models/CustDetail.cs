using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace RdplForm.Models
{
    public class CustDetail
    {
        
        public string CustId { get; set; }
        [Required(ErrorMessage = "Plz provide Name")]
        public string CustName { get; set; }
        [Required (ErrorMessage="Plz Provide Shipping Details")]
        public string Address { get; set; }
        [Required]
        [RegularExpression (@"^\d+$",ErrorMessage="Only Numbers Are Allowed") ]
        public string MobileNO { get; set; }
        public int TotalPrice { get; set; }

    }
}