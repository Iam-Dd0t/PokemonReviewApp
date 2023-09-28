using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Data;
using PokemonReviewApp.IRepository;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class PokemonRepo: IPokemon
    {
        private readonly DatabaseContext _context;
        public PokemonRepo(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Pokemon> Create(Pokemon entity)
        {
            var result = await _context.Pokemon.AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            var result = await _context.Pokemon.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
               _context.Pokemon.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<Pokemon> GetPokemonById(int id)
        {
            return await _context.Pokemon.FirstOrDefaultAsync(e => e.Id == id);
        }

        public  async Task<IEnumerable<Pokemon>> GetPokemons()
        {
            return await _context.Pokemon.AsNoTracking().ToListAsync() ;
        }

        public async void Update(Pokemon entity)
        {
            _context.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
    
}
