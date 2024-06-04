using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_Models.Models
{
    public class ShoppingCart : BaseModel
    {
        [Range(1, 100, ErrorMessage = "Please enter number range between 1 - 1000")]
        public int Count { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Guid? ProductId { get; set; }//
        public Product? Product { get; set; }

        [ForeignKey("ApplicationUseId")]
        public Guid? ApplicationUseId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

    }
}
