using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data.Interfases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoriesExists(int id)
        {
            if (_context.Categories.Any(c => c.Id == id))
                return true;
            return false;
        }

        public bool CreateCategory(Category category)
        {
            _context.Add(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _context.Remove(category);
            return Save();
        }

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories.AsNoTracking();
            return categories.ToList();
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories.AsNoTracking().Where(c => c.Id == id).FirstOrDefault();
            return category;
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.AsNoTracking().Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCategory(Category category)
        {
            _context.Update(category);
            return Save();
        }
    }
}
