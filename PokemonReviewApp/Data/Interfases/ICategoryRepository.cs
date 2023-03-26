using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interfases
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        ICollection<Pokemon> GetPokemonByCategory(int categoryId);
        bool CategoriesExists(int id);  
        Category GetCategory(int id);
    }
}
