using BookSell.DataAccess.Repository;
using BookSell.DataAccess.Repository.IRepository;
using BookSell.Models;
using BookSell.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BookSell.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _IwebEnvironment;

        public ProductController(IUnitOfWork unit, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unit;
            _IwebEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> booklist = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();

            return View(booklist);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new()
            {
                // using projection here for dynamic conversion
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                // create funtionality
                return View(productVM);
            } else
            {
                // update funtionality
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }

            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVm, IFormFile? file)
        {
          

            if (ModelState.IsValid)
            {
                string wwwRootPath = _IwebEnvironment.WebRootPath;
                 if (file != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\products");

                    if (!string.IsNullOrEmpty(productVm.Product.ImageURL))
                    {
                        // delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ImageURL.Trim('\\'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // Save the file with the original file name
                    using (var stream = new FileStream(Path.Combine(productPath, filename), FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    productVm.Product.ImageURL = @"\images\products\" + filename;

                }

                 if (productVm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                } else
                {
                    _unitOfWork.Product.Update(productVm.Product);
                }

               
                _unitOfWork.Save();
                TempData["success"] = "A new book created successfully";
                return RedirectToAction("Index");
            } else
            {
                productVm.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });

                return View(productVm);
            }

        }


        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Product> booklist = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new {data =  booklist});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);

            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            string productPath = @"images\products\product-" + id;

            string oldImagePath = Path.Combine(_IwebEnvironment.WebRootPath, productToBeDeleted.ImageURL.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }


            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
