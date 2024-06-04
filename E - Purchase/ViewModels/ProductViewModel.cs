using E_Purchase_Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace E___Purchase.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategoryList { get;}
    }
}
