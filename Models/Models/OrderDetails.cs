using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_Models.Models
{
    public class OrderDetail : BaseModel
    {
        public Guid OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeaders { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }
	}
}
