using PokemonReviewApp.Models;

namespace PokemonReviewApp.IRepository
{
    public interface IPokemon
    {
       Task <IEnumerable<Pokemon>> GetPokemons();
       Task <Pokemon> GetPokemonById(int id);
       Task <Pokemon> Create(Pokemon entity);
       void Update(Pokemon entity); 
       Task<bool> Delete(int id);
    }
}
