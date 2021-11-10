using ApplicationCore.Entities;
using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IMovieRepository _movieRepository;
        public UserService(IUserRepository userRepository, IPurchaseRepository purchaseRepository, IFavoriteRepository favoriteRepository, IMovieRepository movieRepository)
        {
            _userRepository = userRepository;
            _purchaseRepository = purchaseRepository;
            _favoriteRepository = favoriteRepository;
            _movieRepository = movieRepository;
        }

        public async Task<int> RegisterUser(UserRegisterRequestModel requestModel)
        {
            // check whether email exists in the database
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser != null)
                //email exists in the database
                throw new Exception("Email already exists, please login");

            // generate a random unique salt
            var salt = GetSalt();

            // create the hashed password with salt generated in the above step
            var hashedPassword = GetHashedPassword(requestModel.Password, salt);

            // save the user object to db
            // create user entity object
            var user = new User
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Email = requestModel.Email,
                Salt = salt,
                HashedPassword = hashedPassword,
                DateOfBirth = requestModel.DateOfBirth
            };

            // use EF to save this user in the user table
            var newUser = await _userRepository.Add(user);
            return newUser.Id;
        }

        private string GetSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA512,
                10000,
                256 / 8));
            return hashed;
        }

        public async Task<UserLoginResponseModel> LoginUser(UserLoginRequestModel requestModel)
        {
            // get the salt and hashedpassword from databse for this user
            var dbUser = await _userRepository.GetUserByEmail(requestModel.Email);
            if (dbUser == null) throw null;

            // hash the user entered password with salt from the database

            var hashedPassword = GetHashedPassword(requestModel.Password, dbUser.Salt);
            // check the hashedpassword with database hashed password
            if (hashedPassword == dbUser.HashedPassword)
            {
                // user entered correct password
                var userLoginResponseModel = new UserLoginResponseModel
                {
                    Id = dbUser.Id,
                    FirstName = dbUser.FirstName,
                    LastName = dbUser.LastName,
                    DateOfBirth = dbUser.DateOfBirth.GetValueOrDefault(),
                    Email = dbUser.Email
                };
                return userLoginResponseModel;
            }

            return null;
        }

        public async Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            var newFavorite = await _favoriteRepository.Add(new Favorite
            {
                MovieId = favoriteRequest.MovieId,
                UserId = favoriteRequest.UserId
            });
        }

        public async Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            //var removeFavorite = await _favoriteRepository.Delete(Favorite
            //{
            //    MovieId = favoriteRequest.MovieId,
            //    UserId = favoriteRequest.UserId
            //});
            throw new NotImplementedException();
        }

        public async Task<FavoriteResponseModel> GetAllFavoritesForUser(int id)
        {
            var favorites = await _favoriteRepository.GetAll();
            if (favorites == null) throw new Exception("No favorite movies found");
            var favoriteMovie = new FavoriteResponseModel();
            var favoriteCard = new MovieCardResponseModel();
            favoriteMovie.UserId = id;
            foreach (var favorite in favorites)
            {
                favoriteCard.Id = favorite.Id;
                var movie = _movieRepository.GetMovieById(favorite.MovieId);
                favoriteCard.PosterUrl = movie.Result.PosterUrl;
                favoriteCard.Title = movie.Result.Title;
                favoriteMovie.FavoriteMovies.Add((FavoriteResponseModel.FavoriteMovieResponseModel)favoriteCard);
            }

            return favoriteMovie;
        }

        //public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        //{
        //    if (purchaseRequest == null)
        //    {
        //        throw new Exception($"No User Found for this {userId}");
        //    }
        //    var purchase = new Purchase
        //    {
        //        UserId = userId,
        //        PurchaseNumber = (Guid)purchaseRequest.PurchaseNumber,
        //        PurchaseDateTime = (DateTime)purchaseRequest.PurchaseDateTime,
        //        MovieId = purchaseRequest.MovieId
        //    };
        //    var newPurchase = await _purchaseRepository.Add(purchase);
        //    return newPurchase != null;
        //}

        //public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        //{
        //    var dbPurchase = await _purchaseRepository.GetPurchaseDetails(userId, purchaseRequest.MovieId);
        //    if (dbPurchase == null)
        //    {
        //        throw new Exception($"No User Found for this {userId}");
        //    }
        //    if (purchaseRequest.PurchaseNumber == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        //public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int userId)
        //{
        //    var purchases = await _purchaseRepository.GetAllPurchasesForUser(userId);

        //    if (purchases == null)
        //    {
        //        throw new Exception($"No movie has been purchased by this user {userId}");
        //    }

        //    var purchasedMovies = new List<MovieCardResponseModel>();
        //    foreach (var purchase in purchases)
        //    {
        //        //var movie = await _movieRepository.GetMovieById(purchase.MovieId);
        //        var movieCard = new MovieCardResponseModel
        //        {
        //            Id = purchase.MovieId,
        //            Title = purchase.Movie.Title,
        //            PosterUrl = purchase.Movie.PosterUrl
        //        };
        //        purchasedMovies.Add(movieCard);
        //    }

        //    var purchaseResponse = new PurchaseResponseModel
        //    {
        //        PurchasedMovies = purchasedMovies,
        //        TotalMoviesPurchased = purchases.Count()

        //    };

        //    return purchaseResponse;
        //}

        public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        {
            var dbUser = await _purchaseRepository.GetPurchaseDetails(userId, movieId);
            if (dbUser == null)
            {
                throw new Exception($"No Purchase for this {movieId} Found for this {userId}");
            }

            var getPurchaseDetails = new PurchaseDetailsResponseModel
            {
                Id = dbUser.Id,
                UserId = dbUser.UserId,
                PurchaseNumber = dbUser.PurchaseNumber,
                TotalPrice = dbUser.TotalPrice,
                PurchaseDateTime = dbUser.PurchaseDateTime,
                MovieId = dbUser.MovieId,
                Title = dbUser.Movie.Title,
                PosterUrl = dbUser.Movie.PosterUrl,
                ReleaseDate = dbUser.Movie.ReleaseDate
            };
            return getPurchaseDetails;
        }

        public async Task<PurchaseResponseModel> GetAllPurchasesForUser(int userId)
        {
            var purchases = await _purchaseRepository.GetAllPurchasesForUser(userId);

            if (purchases == null)
            {
                throw new Exception($"No movie has been purchased by this user {userId}");
            }

            var purchasedMovies = new List<MovieCardResponseModel>();
            foreach (var purchase in purchases)
            {
                //var movie = await _movieRepository.GetMovieById(purchase.MovieId);
                var movieCard = new MovieCardResponseModel
                {
                    Id = purchase.MovieId,
                    Title = purchase.Movie.Title,
                    PosterUrl = purchase.Movie.PosterUrl
                };
                purchasedMovies.Add(movieCard);
            }

            var purchaseResponse = new PurchaseResponseModel
            {
                PurchasedMovies = purchasedMovies,
                TotalMoviesPurchased = purchases.Count()

            };

            return purchaseResponse;
        }

        public async Task<bool> PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {

            var purchase = new Purchase
            {
                UserId = userId,
                PurchaseNumber = (Guid)purchaseRequest.PurchaseNumber,
                PurchaseDateTime = (DateTime)purchaseRequest.PurchaseDateTime,
                MovieId = purchaseRequest.MovieId
            };
            var newPurchase = await _purchaseRepository.Add(purchase);
            return newPurchase != null;
        }

        public async Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            var isPurchased = await _purchaseRepository.GetPurchaseDetails(userId, purchaseRequest.MovieId);
            return isPurchased != null;
        }

        //public async Task<PurchaseDetailsResponseModel> GetPurchasesDetails(int userId, int movieId)
        //{
        //    var purchaseDetail = await _purchaseRepository.GetPurchaseDetails(userId, movieId);
        //    var purchaseDetailsResponseModel = new PurchaseDetailsResponseModel
        //    {
        //        UserId = userId,
        //        MovieId = movieId,
        //        Title = purchaseDetail.Movie.Title,
        //        PosterUrl = purchaseDetail.Movie.PosterUrl,
        //        ReleaseDate = purchaseDetail.Movie.ReleaseDate,
        //        PurchaseDateTime = purchaseDetail.PurchaseDateTime,
        //        TotalPrice = purchaseDetail.TotalPrice
        //    };

        //    return purchaseDetailsResponseModel;

        //}

        public async Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            var dbReview = await _userRepository.GetReviewsByUser(reviewRequest.UserId);
            if (dbReview == null)
            {
                throw new Exception($"No User Found for this {reviewRequest.UserId}");
            }

            var addReview = new ReviewRequestModel
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
        }

        public async Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            var dbUpdate = await _userRepository.GetById(reviewRequest.UserId);
            if (dbUpdate == null)
            {
                throw new Exception($"No User Found for this {reviewRequest.UserId}");
            }

            var updateReview = new ReviewRequestModel
            {
                UserId = reviewRequest.UserId,
                MovieId = reviewRequest.MovieId,
                ReviewText = reviewRequest.ReviewText,
                Rating = reviewRequest.Rating
            };
        }

        public async Task DeleteMovieReview(int userId, int movieId)
        {

            var dbDelete = await _userRepository.GetById(userId);
            if (dbDelete == null)
            {
                throw new Exception($"No User Found for this {userId}");
            }

            var deleteReview = new ReviewRequestModel
            {
                UserId = userId,
                MovieId = movieId
            };
        }


    }
}
