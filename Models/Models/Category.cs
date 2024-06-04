using E_Purchase_Models.Models;
using System.ComponentModel.DataAnnotations;
using E_Purchase_Models;

namespace E_Purchase_Models
{
    public class Category : BaseModel
    {
        [Required]
        [Display(Name = "Display Name")]
        public string Name { get; set; }
        [Range(0, 100, ErrorMessage = "The field Display Order must be between 1 - 100.")]
        [Display(Name = "Display Order")]
        public int  DisplayOrder { get; set; }

    }
}
