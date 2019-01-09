using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RdplForm.Models
{
    public class CartItemsViewModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string CompanyName { get; set; }
        public string ProdDescription { get; set; }
        public string Images { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int Total { get; set; }
    }
}