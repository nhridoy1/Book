using BookSell.DataAccess.Data;
using BookSell.DataAccess.Repository.IRepository;
using BookSell.Models;
using System.Linq.Expressions;


namespace BookSell.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        
        public void Update(Category obj)
        {
           _db.Categories.Update(obj);
        }
    }
}
