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

        public ProductController(IUnitOfWork unit)
        {
            _unitOfWork = unit;
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
                _unitOfWork.Product.Add(productVm.Product);
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
