using E_PurchaseRazor_Temp.Data;
using E_PurchaseRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_PurchaseRazor_Temp.Pages.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category? Categorys { get; set; }
        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(Guid id)
        {
            if (id != null)
            {
                Categorys = _db.Categories.FirstOrDefault(c => c.Id == id);

            }

        }

        public IActionResult OnPost()
        {
            Category obj = _db.Categories.Find(Categorys.Id);

            if (obj == null)
            {
                NotFound();
            }

            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToPage("Index");



        }
    }
}
