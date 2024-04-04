using BookSellRazor_temp.Data;
using BookSellRazor_temp.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellRazor_temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        
        public Category Category { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
              _db = db;
        }

        
        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.categories.FirstOrDefault(c => c.Id == id);
            }
        }

        public IActionResult OnPost(int? id)
        {
           Category? obj = _db.categories.FirstOrDefault(x => x.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted successfully";
            return RedirectToPage("Index");
        }
    }
}
