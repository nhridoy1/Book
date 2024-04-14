using BookSell.DataAccess.Repository;
using BookSell.DataAccess.Repository.IRepository;
using BookSell.Models;
using BookSell.Models.ViewModels;
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
            List<Product> booklist = _unitOfWork.Product.GetAll().ToList();

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


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Product singleBook = _unitOfWork.Product.Get(u => u.Id == id);

            if (singleBook == null)
            {
                return NotFound();
            }

            return View(singleBook);
        }

   
        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {

            Product? book = _unitOfWork.Product.Get(u => u.Id == id);

            if (book == null)
            {
                NotFound();
            }

            _unitOfWork.Product.Remove(book);
            _unitOfWork.Save();
            TempData["success"] = "Book deleted";
            return RedirectToAction("Index", "Product");
        }
    }
}
