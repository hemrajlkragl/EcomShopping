﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcomShopping.Models
{
    public class OrderDetails
    {
        public int Id { get; set; }
        [Display(Name ="Order")]
        public int OrderId { get; set; }
        [Display(Name = "Product")]

        public int ProductId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Products Products { get; set; }
    }
}
