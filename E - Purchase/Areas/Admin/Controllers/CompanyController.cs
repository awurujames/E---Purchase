using E_Purchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_Models;
using E_Purchase_Models.Models;
using E_Purchase_Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E___Purchase.Areas.Admin.Controllers
{
    [Area("Admin")]
   // [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(ILogger<CompanyController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Company> productList = _unitOfWork.Company.GetAll().ToList();
            return View(productList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Company obj)
        {

            if (obj != null && ModelState.IsValid)
            {
                _unitOfWork.Company.Add(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Comapany created successfully";
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
            var obj = _unitOfWork.Company.Get(x => x.Id == id);

            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        public IActionResult Edit(Company obj)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Company.Update(obj);
                _unitOfWork.Save();
                TempData["Success"] = "Company updated successfully";
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
            var obj = _unitOfWork.Company.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(Guid? id)
        {
            var obj = _unitOfWork.Company.Get(x => x.Id == id);
            if (obj == null)
            {
                return NotFound(id);
            }
            _unitOfWork.Company.Remove(obj);
            _unitOfWork.Save();
            TempData["Success"] = "Company deleted successfully";
            return RedirectToAction("Index");
        }


        #region API CALL
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> prodList = _unitOfWork.Company.GetAll().ToList();

            return Json(new { data = prodList });
        }
        #endregion

    }
}
