using E___Purchase.Models;
using E_Purchase_Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Purchase_Models.Models
{
    public class Product : BaseModel
    {
        [DisplayName("Product Name")]
        [Required]
        public string? Title { get; set; }

        [Required]
        public string  ISBN { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]  
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public double ListPrice { get; set; }

        [Required]
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public double Price { get; set; }

        [Required]
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public double Price50 { get; set; }

        [Required]
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public double Price100 { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}
