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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetGenres();
            if (genres == null)
            {
                throw new Exception($"No Genres Found");
            }

            var allGenres = new List<GenreModel>();
            foreach (var genre in genres)
            {
                allGenres.Add(new GenreModel
                {
                    Id = genre.Id,
                    Name = genre.Name
                });
            }

            return allGenres;
        }

        //public async Task<List<GenreModel>> GetAllGenres()
        //{
        //    var genres = await _genreRepository.GetGenres();
        //    if (genres == null)
        //    {
        //        throw new Exception($"No Genres Found");
        //    }

        //    var movieCards = new List<MovieCardResponseModel>();
        //    foreach (var movie in genres)
        //    {
        //        movieCards.Add(new MovieCardResponseModel
        //        {
        //            Id = movie.Id,
        //            PosterUrl = movie.PosterUrl,
        //            Title = movie.Title
        //        });
        //    }
        //    var allGenres = new List<GenreModel>();
        //    foreach (var genre in genres)
        //    {
        //        allGenres.Add(new GenreModel
        //        {
        //            Id = genre.Id
        //        });
        //    }

        //    return allGenres;
        //}
    }
}
