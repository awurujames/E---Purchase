using E_PurchaseRazor_Temp.Data;
using E_PurchaseRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_PurchaseRazor_Temp.Pages.Categories
{

    public class EditModel : PageModel
    {

        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category? Categorys { get; set; }
        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public void OnGet(Guid id)
        {
            if (id != null)
            {
                Categorys =  _db.Categories.FirstOrDefault(c => c.Id == id);

            }

        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Categorys);
                _db.SaveChanges();
                TempData["Success"] = "Category updated successfully";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}

