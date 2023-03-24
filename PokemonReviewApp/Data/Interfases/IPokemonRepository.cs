using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Interfases
{
    public interface IPokemonRepository
    {
        ICollection<Pokemon> GetPokemons();
        Pokemon GetPokemon(int id);
        Pokemon GetPokemon(string name);
        decimal GetPokimonRaiting(int pokeId);
        bool PokemonExists(int id);
    }
}
