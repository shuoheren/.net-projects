﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<List<MovieCardResponseModel>> GetTop30RevenueMovies()
        {
            var movies = await _movieRepository.GetTop30RevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id, Title = movie.Title, PosterUrl = movie.PosterUrl
                });
            }
            return movieCards;
        }

        public async Task<MovieDetailsResponseModel> GetMovieById(int id)
        {
            var movie = await _movieRepository.GetMovieById(id);
            if (movie == null)
            {
                throw new Exception($"No movie found for this {id}");
            }

            var movieDetails = new MovieDetailsResponseModel
            {
                Id = movie.Id, Budget = movie.Budget, Overview = movie.Overview, Price = movie.Price,
                PosterUrl = movie.PosterUrl, Revenue = movie.Revenue,
                ReleaseDate = movie.ReleaseDate.GetValueOrDefault(), Tagline = movie.Tagline,
                Title = movie.Title, RunTime = movie.RunTime, Rating = movie.Rating,
                BackdropUrl = movie.BackdropUrl, ImdbUrl = movie.ImdbUrl,
                TmdbUrl = movie.TmdbUrl
            };

            foreach (var genre in movie.Genres)
            {
                movieDetails.Genres.Add(new GenreResponseModel
                {
                    Id = genre.GenreId, Name = genre.Genre.Name
                });
            }

            foreach (var cast in movie.Casts)
            {
                movieDetails.Casts.Add(new CastResponseModel
                {
                    Id = cast.CastId, Character = cast.Character,
                    Name = cast.Cast.Name, ProfilePath = cast.Cast.ProfilePath
                });
            }

            foreach (var trailer in movie.Trailers)
            {
                movieDetails.Trailers.Add(new TrailerResponseModel
                {
                    Id = trailer.Id, Name = trailer.Name,
                    MovieId = trailer.MovieId, TrailerUrl = trailer.TrailerUrl
                });
            }

            return movieDetails;
        }
    }
}
