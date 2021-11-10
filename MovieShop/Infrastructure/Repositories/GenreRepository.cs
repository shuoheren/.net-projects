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
    public class GenreRepository : EfRepository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<IEnumerable<Genre>> GetGenres()
        {
            var genres = await _dbContext.Genres.ToListAsync();
            return genres;
        }

        //public async Task<IEnumerable<Movie>> GetGenres()
        //{
        //    var genre = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
        //        .Include(m => m.Genres).ThenInclude(m => m.Genre).Include(m => m.Trailers)
        //        .ToListAsync();
        //    return genre;
        //}

        //public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageIndex = 1)
        //{
        //    var totalMoviesCountByGenre =
        //        await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();

        //    if (totalMoviesCountByGenre == 0) throw new DllNotFoundException("NO Movies found for this genre");
        //    var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).Include(g => g.Movie).OrderByDescending(m => m.Movie.Revenue)
        //        .Select(m => new Movie
        //        {
        //            Id = m.MovieId,
        //            PosterUrl = m.Movie.PosterUrl,
        //            Title = m.Movie.Title,
        //            ReleaseDate = m.Movie.ReleaseDate
        //        })
        //        .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        //    return new IEnumerable<Movie>(movies, pageIndex, pageSize, totalMoviesCountByGenre);
        //}
    }
}
