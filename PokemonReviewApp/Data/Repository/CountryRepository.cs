using PokemonReviewApp.Data.Interfases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        public CountryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CountryExists(int id)
        {
            if (_context.Countries.Any(c => c.Id == id)) return true;
            return false;
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country);
            return Save();

        }

        public bool DeleteCountry(Country country)
        {
            _context.Remove(country);
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList();
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault();
        }

        public Country GetCountryByOwner(int ownerId)
        {
            var country = _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault();
            return country;
        }

        public ICollection<Owner> GetOwnersFromCountry(int countryId)
        {
            var owners = _context.Owners.Where(c => c.Country.Id == countryId);
            return owners.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCountry(Country country)
        {
            _context.Update(country);
            return Save();
        }
    }
}
