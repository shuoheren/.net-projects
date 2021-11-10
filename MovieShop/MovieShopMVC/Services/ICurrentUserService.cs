using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieShopMVC.Services
{
    public interface ICurrentUserService
    {
        // expose some properties and methods that can be implemented by CurrentUserService class
        // that will read user info from HttpContext 

        public int UserId { get; }
        public bool IsAuthenticated { get; }
        public string FullName { get; }
        public string Email { get; }
        public IEnumerable<string> Roles { get; }
        public bool IsAdmin { get; }

        public DateTime DateOfBirth { get; }

        //Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequestModel, int userId);
        //Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequestModel, int userId);
        //Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId);
        //Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);

        //Task AddFavorite(FavoriteRequestModel favoriteRequest);
        //Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        //Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);


        //Task AddMovieReview(ReviewRequestModel reviewRequest);
        //Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        //Task DeleteMovieReview(int userId, int movieId);
        //Task<ReviewResponseModel> GetAllReviewsByUser(int id);
    }
}