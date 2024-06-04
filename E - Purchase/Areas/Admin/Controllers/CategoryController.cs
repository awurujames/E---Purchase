
using E_Purchase_DataAccess.AllRepository.IRepositories;
using Microsoft.AspNetCore.Mvc;
using E_Purchase_Models;
using Microsoft.AspNetCore.Authorization;
using E_Purchase_Utility;

namespace E___Purchase.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var obj = _unitOfWork.Category.GetAll().ToList();
            return View(obj);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj != null && obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "Category Name and Display Order cannot be the same");
            }

            if (obj != null && ModelState.IsValid)
            {
                _unitOfWork.Category.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }


        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Category.Get(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var obj = _unitOfWork.Category.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _unitOfWork.Category.Get(x => x.Id == id);
            if (obj == null)
            {
                return NotFound(id);
            }
            _unitOfWork.Category.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
