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

        public ICollection<Category> GetCategories()
        {
            var categories = _context.Categories;
            return categories.ToList();
        }

        public Category GetCategory(int id)
        {
            var category = _context.Categories.Where(c => c.Id == id).FirstOrDefault();
            return category;
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }
    }
}
