using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Movie> GetMovieById(int id)
        {
             var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
                .SingleOrDefaultAsync(m => m.Id == id);
            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            // First vs FirstOrDefault
            // Single ( should be only 1  0, more than 1 exception)
            // vs SingleOrDefault(0, 1 more than 1 exception)

            return movie;

        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            // we are gonna use EF with LINQ to get top 30 movies by revenue
            // SQL select top 30 * from Movies order by Revenue
            // I/O bound operation
            // u can await only Tasks
            // EF and Dapper have both sync and async methods
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1)
        {
            var movieReview = await _dbContext.Reviews.Include(r => r.MovieId).Include(r => r.UserId)
                .Include(r => r.Rating).Include(r => r.ReviewText).SingleOrDefaultAsync(r => r.MovieId == id);
            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == id).DefaultIfEmpty()
                .AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movieReview.Rating = movieRating;
            return (IEnumerable<Review>)movieReview;
            
        }

        public async Task<IEnumerable<Review>> GetTop30RatedMovies()
        {
            var movies = await _dbContext.Reviews.OrderByDescending(r => r.Rating)
                .Take(30).ToListAsync();
            return movies;
        }

        public async Task<Genre> GetGenre(int id)
        {
            var genre = await _dbContext.Genres.Include(g => g.Movies)
                .SingleOrDefaultAsync(g => g.Id == id);
            return genre;
        }

        public async Task<Review> GetReviews(int id)
        {
            var review = await _dbContext.Reviews.Include(r => r.Movie)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.MovieId == id);
            return review;
        }

        public async Task<IEnumerable<Movie>> GetMovie()
        {
            var movie = await _dbContext.Movies.Take(30)
                .ToListAsync();

            // First vs FirstOrDefault
            // Single ( should be only 1  0, more than 1 exception)
            // vs SingleOrDefault(0, 1 more than 1 exception)

            return movie;
        }
    }
}
