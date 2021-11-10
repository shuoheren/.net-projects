using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {
       Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();
        Task<MovieDetailsResponseModel> GetMovieDetails(int id);

        Task<List<ReviewRequestModel>> GetTop30RatedMovies();
        Task<GenreModel> GetGenre(int id);
        Task<MovieReviewResponseModel> GetReviewsById(int id);
        Task<List<MovieCardResponseModel>> GetAllMovies();
    }
}
