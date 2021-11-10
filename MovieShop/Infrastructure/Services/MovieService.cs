using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<GenreModel> GetGenre(int id)
        {
            var genre = await _movieRepository.GetGenre(id);
            if (genre == null)
            {
                // throw new Exception($"No Genre Found for this {id}");
                return null;
            }

            var movieGenre = new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name

            };
            return movieGenre;
        }

        public async Task<MovieDetailsResponseModel> GetMovieDetails(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                // throw new Exception($"No Movie Found for this {id}");
                return null;
            }

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id,
                Budget = movie.Budget,
                Overview = movie.Overview,
                Price = movie.Price,
                PosterUrl = movie.PosterUrl,
                Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(),
                Rating = movie.Rating,
                Tagline = movie.Tagline,
                Title = movie.Title,
                RunTime = movie.RunTime,
                BackdropUrl = movie.BackdropUrl,
                ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
              
            };

            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(
                    new GenreModel
                    {
                        Id = genre.GenreId,
                        Name = genre.Genre.Name
                    });
            }

            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(
                    new CastResponseModel
                    {
                        Id = cast.CastId,
                        Name = cast.Cast.Name,
                        ProfilePath = cast.Cast.ProfilePath
                    });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(
                    new TrailerResponseModel
                    {
                        Id = trailer.Id,
                        TrailerUrl = trailer.TrailerUrl,
                        Name = trailer.Name,
                        MovieId = trailer.MovieId
                    });
            }

            return movieDetails;
        }

        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            // calling MovieRepository with DI based on IMovieRepository
            // I/O bound operation
            var movies = await _movieRepository.GetTop30RevenueMovies();

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;
        }

        public async Task<List<ReviewRequestModel>> GetTop30RatedMovies()
        {
            // calling MovieRepository with DI based on IMovieRepository
            // I/O bound operation
            var movies = await _movieRepository.GetTop30RatedMovies();

            var movieCards = new List<ReviewRequestModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new ReviewRequestModel
                {
                    UserId = movie.UserId,
                    MovieId = movie.MovieId,
                    Rating = movie.Rating,
                    ReviewText = movie.ReviewText
                });
            }

            return movieCards;
        }

        public async Task<MovieReviewResponseModel> GetReviewsById(int id)
        {
            var review = await _movieRepository.GetReviews(id);
            if (review == null)
            {
                throw new Exception($"No Movie Found for this {id}");
            }

            var movieReview = new MovieReviewResponseModel
            {
                UserId = review.UserId,
                MovieId = review.MovieId,
                ReviewText = review.ReviewText,
                Rating = review.Rating,
                Name = review.User.FirstName
            };
            return movieReview;
        }

        public async Task<List<MovieCardResponseModel>> GetAllMovies()
        {
            var movies = await _movieRepository.GetMovie();
            if (movies == null)
            {
                throw new Exception($"No Movies Found");
            }

            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }

            return movieCards;

            //var allMovies = new MovieDetailsResponseModel
            //{
            //    Id = movies.Id,
            //    Budget = movies.Budget,
            //    Overview = movies.Overview,
            //    Price = movies.Price,
            //    PosterUrl = movies.PosterUrl,
            //    Revenue = movies.Revenue,
            //    ReleaseDate = movies.ReleaseDate.GetValueOrDefault(),
            //    Rating = movies.Rating,
            //    Tagline = movies.Tagline,
            //    Title = movies.Title,
            //    RunTime = movies.RunTime,
            //    BackdropUrl = movies.BackdropUrl,
            //    ImdbUrl = movies.ImdbUrl,
            //    TmdbUrl = movies.TmdbUrl
            //};
            //return allMovies;
        }
    }
}
