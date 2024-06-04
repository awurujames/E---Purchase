using E_PurchaseRazor_Temp.Data;
using E_PurchaseRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_PurchaseRazor_Temp.Pages.Categories
{
    
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Category> CategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            CategoryList = _db.Categories.ToList(); 
        }
    }
}
