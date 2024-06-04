using E_Purchase_DataAccess.AllRepository.IRepositories;
using E_Purchase_Models.Models;
using E_Purchase_Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Drawing;

namespace E___Purchase.Areas.Customer.Controllers;

[Area("Customer")]
public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public  IActionResult Index()
        {
            List<Product> prodList =  _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            //IEnumerable<SelectListItem> catListItem = _unitOfWork.Category.GetAll().Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString(),
            //});
            
            return View(prodList);
        }

        public IActionResult Upsert(Guid id)
        {
            ProductViewModel productViewModel = new ProductViewModel()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                }),
                Product = new Product()

            };
            if (id == Guid.Empty)
            {
                return View(productViewModel);
            }
            else
            {
                productViewModel.Product = _unitOfWork.Product.Get(x => x.Id == id);
                
                return View(productViewModel);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductViewModel prodVM, IFormFile file)
        {
            if(ModelState.IsValid)
            {
                string wwwRoothPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string rootPath = Path.Combine(wwwRoothPath, @"Images/Product");

                    if (!string.IsNullOrEmpty(prodVM.Product.ImageUrl))
                    {
                        var oldImg = Path.Combine(prodVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImg))
                        {
                            System.IO.File.Delete(oldImg);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(rootPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    prodVM.Product.ImageUrl = @"/Images/Product/" + fileName;
                }

                if (prodVM.Product.Id == null)
                {
                    _unitOfWork.Product.Add(prodVM.Product);
                    _unitOfWork.Save();

                    TempData["Success"] = "Product created successfully";
                    return RedirectToAction("Index");
                }
                else
                {
                    _unitOfWork.Product.Update(prodVM.Product);
                    _unitOfWork.Save();

                    TempData["Success"] = "Product updated successfully";
                    return RedirectToAction("Index");
                }
                
            }
            else
            {
                prodVM.CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString(),
                });

                return View(prodVM);
            }
        }




    //public IActionResult delete(Guid id)
    //{
    //    Product productViewModel = new Product()
    //    {
    //        //CategoryList = _unitOfWork.Category.GetAll().Select(c => new SelectListItem
    //        //{
    //        //    Text = c.Name,
    //        //    Value = c.Id.ToString(),
    //        //}),
    //        //Product = new Product()

    //    };
    //    //return View(productViewModel);
    //    productViewModel = _unitOfWork.Product.Get(x => x.Id == id);

    //    return View(productViewModel);
    //}

    //[HttpGet]
    //public IActionResult Delete(Guid id)
    //{
    //    if (id == Guid.Empty)
    //    {
    //        return NotFound();
    //    }
    //    Product objFromDb = _unitOfWork.Product.Get(x => x.Id == id);
    //    if (objFromDb == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(objFromDb);
    //}




    //[HttpPost, ActionName("Delete") ]
    //public IActionResult DeletePost(Guid id)
    //{
    //    if(id == Guid.Empty)
    //    {
    //        return NotFound();
    //    }
    //    var objFromDb = _unitOfWork.Product.Get(x => x.Id == id);
    //    if(objFromDb == null)
    //    {
    //        return NotFound();
    //    }
    //    _unitOfWork.Product.Remove(objFromDb);
    //    _unitOfWork.Save();
    //    TempData["Success"] = "Product deleted successfully";
    //    return RedirectToAction("Index");
    //}


 
    public IActionResult Delete(Guid Id)
    {
        var prodToBeDeleted = _unitOfWork.Product.Get(x => x.Id == Id);
        if(prodToBeDeleted == null)
        {
            return Json(new { success = false, message = "error while deleting" });
        }

        var oldImg = Path.Combine(_webHostEnvironment.WebRootPath, @"Images/Product");

        if(oldImg == null)
        {
            return NotFound();
        }

        try
        {
            if (System.IO.File.Exists(oldImg))
            {
                System.IO.File.Delete(oldImg);
            } 
        }
        catch (Exception ex)
        {
            // Log the exception or handle it appropriately
            Console.WriteLine($"Error deleting file: {ex.Message}");
        }

        _unitOfWork.Product.Remove(prodToBeDeleted);
        _unitOfWork.Save();

        return Json(new { success = true, message = "Record Deleted successfuly!" });
    }

    #region API CALL
    [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> prodList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return Json(new { data = prodList });
        }
        #endregion


}

