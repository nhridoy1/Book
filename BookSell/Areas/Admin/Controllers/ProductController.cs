using BookSell.DataAccess.Repository;
using BookSell.DataAccess.Repository.IRepository;
using BookSell.Models;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (product.Title == product.Description.ToString())
            {
                ModelState.AddModelError("Name", "book name and description can't be same");
            }

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(product);
                _unitOfWork.Save();
                TempData["success"] = "A new book created successfully";
                return RedirectToAction("Index", "Product");
            }

            return View();
        }


        public IActionResult Edit(int? id)
        {

            if (id == null && id == 0)
            {
                return NotFound();
            }

            Product? singleBook = _unitOfWork.Product.Get(u => u.Id == id);

            if (singleBook == null)
            { 
                return NotFound(); 
            }


            return View(singleBook);
        }

        [HttpPost]
        public IActionResult Edit(Product bookObj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(bookObj);
                _unitOfWork.Save();
                TempData["success"] = "Book is updated";
                return RedirectToAction("Index", "Product");
            }

            return View();
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
