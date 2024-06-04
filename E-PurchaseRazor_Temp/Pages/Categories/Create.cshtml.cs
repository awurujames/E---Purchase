using E_PurchaseRazor_Temp.Data;
using E_PurchaseRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_PurchaseRazor_Temp.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
     
        public Category category { get; set; }
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(Category category)
        {
        }
        public IActionResult OnPost( Category category)
        {
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Add(category);
            _db.SaveChanges();
            TempData["Success"] = "Category Created successfully";
            return RedirectToPage("Index");   
        }

        
    }
}
