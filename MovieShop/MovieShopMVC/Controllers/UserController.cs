using System;
using System.Security.Claims;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;

namespace MovieShopMVC.Controllers
{
    // all the action methods in User Controller should work only when user is Authenticated (login success)
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;
        public UserController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(int movieId)
        {
            // purchase a movie when user clicks on Buy button on Movie Details Page
            var purchaseRequestModel = new PurchaseRequestModel
            {
                MovieId = movieId
            };
            // is purchased
            var isPurchased = await _userService.IsMoviePurchased(purchaseRequestModel, _currentUserService.UserId);
            if (isPurchased)
            {
                return RedirectToAction("Details", "Movies", new { id = movieId });
            }

            var succeedPurchased = await _userService.PurchaseMovie(purchaseRequestModel, _currentUserService.UserId);
            if (succeedPurchased)
            {
                return RedirectToAction("Purchases");
            }
            return RedirectToAction("Details", "Movies");
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(FavoriteRequestModel favoriteRequest)
        {
            // favorite a movie when user clicks on Favorite Button on Movie Details Page
            var favorites = _userService.AddFavorite(favoriteRequest);
            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // add a new review done by the user for that movie
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> PurchaseDetails(int movieId)
        {
            var userId = _currentUserService.UserId;
            var purchaseDetails = await _userService.GetPurchasesDetails(userId, movieId);
            // return a partial view, put the partial view inside the popup (bootstrap model) 
            return View(purchaseDetails);
        }

        [HttpGet]
        // Filters in ASP.NET 
        [Authorize]
        public async Task<IActionResult> Purchases(int id, PurchaseRequestModel purchaseRequest)
        {
            // get the id from HttpCOntext.User.Claims
            /*var userIdentity = this.User.Identity;
            if (userIdentity != null && userIdentity.IsAuthenticated)
            {
                // call the databsae to get the data
                return View();
            }
            

            RedirectToAction("Login", "Account");*/
            // get all the movies purchased by user => List<MovieCard> 

            //int userId = Convert.ToInt32((HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            // call userservie that will give list od moviesCard Models that this user purchased
            // Purchase, dbContext.Purchase.where(u=> u.UserId == id);
            var userId = _currentUserService.UserId;
            var purchaseDetails = await _userService.GetAllPurchasesForUser(userId);
            //ViewBag.UserId = userId;
            // call the USer
            return View(purchaseDetails);

        }

        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            // get all movies favorited by that user
            //var favorite = _currentUserService.UserId == id;
            var favorites = await _userService.GetAllFavoritesForUser(id);
            return View(favorites.FavoriteMovies);
        }

        public async Task<IActionResult> Reviews(int id)
        {
            // get all the reviews done by this user
            return View();
        }
    }
}