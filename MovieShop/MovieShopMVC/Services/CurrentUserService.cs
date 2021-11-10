using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;

namespace MovieShopMVC.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

        }

        // we need to use HttpContext class to get all this information from HttpContext User Object

        public int UserId => Convert.ToInt32((_httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value));

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity != null &&
                                       _httpContextAccessor.HttpContext != null &&
                                       _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;

        public string FullName => _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value
                                  + " " + _httpContextAccessor.HttpContext?.User.Claims
                                      .FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value;


        public string Email => _httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

        public IEnumerable<string> Roles { get; }
        public bool IsAdmin { get; }

        public DateTime DateOfBirth => Convert.ToDateTime(_httpContextAccessor.HttpContext?.User.Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.DateOfBirth)?.Value);

        


        //public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        //{
        //    throw new NotImplementedException();
        //}



        //public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task DeleteMovieReview(int userId, int movieId)
        //{
        //    throw new NotImplementedException();
        //}

        //public async Task<ReviewResponseModel> GetAllReviewsByUser(int id)
        //{
        //    throw new NotImplementedException();
        //}
    }
}