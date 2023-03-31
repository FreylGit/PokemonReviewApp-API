using AutoMapper;
using PokemonReviewApp.Data.Interfases;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Data.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReview(int reviewerId, int pokeId, Review review)
        {
            var reviewer = _context.Reviewers.Where(r => r.Id == reviewerId).FirstOrDefault();
            var pokemon = _context.Pokemons.Where(p => p.Id == pokeId).FirstOrDefault();

            if (reviewer != null && pokemon != null)
            {
                review.Pokemon = pokemon;
                review.Reviewer = reviewer;
                _context.Add(review);
                return Save();
            }
            return false;
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r => r.Id == reviewId).FirstOrDefault();
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfAPokemon(int pokeId)
        {
            return _context.Reviews.Where(r => r.Pokemon.Id == pokeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r => r.Id == reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReview(Review review)
        {
            _context.Update(review);
            return Save();
        }
    }
}
