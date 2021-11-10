using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IUserService
    {
        Task<int> RegisterUser(UserRegisterRequestModel requestModel);
        Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel);

        Task AddFavorite(FavoriteRequestModel favoriteRequest);
        Task RemoveFavorite(FavoriteRequestModel favoriteRequest);
        Task<FavoriteResponseModel> GetAllFavoritesForUser(int id);

        Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequestModel, int userId);
        Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequestModel, int userId);
        Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId);
        Task<PurchaseResponseModel> GetAllPurchasesForUser(int id);


        Task AddMovieReview(ReviewRequestModel reviewRequest);
        Task UpdateMovieReview(ReviewRequestModel reviewRequest);
        Task DeleteMovieReview(int userId, int movieId);
        //Task<UserReviewResponseModel> GetAllReviewsByUser(int id);


    }
}
