using BookSellRazor_temp.Data;
using BookSellRazor_temp.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BookSellRazor_temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public List<Category> myCategoryList { get; set; }

        public IndexModel(ApplicationDbContext db)
        {
                  _db = db;
        }

        public void OnGet()
        {
            myCategoryList = _db.categories.ToList();
        }
    }
}
