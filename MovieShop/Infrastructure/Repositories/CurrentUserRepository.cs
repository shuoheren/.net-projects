using ApplicationCore.Entities;
using ApplicationCore.Models;
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
    public class CurrentUserRepository: ICurrentUserRepository
    {
        public MovieShopDbContext _dbContext;
        public CurrentUserRepository(MovieShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public async Task<Purchase> IsMoviePurchased(PurchaseRequestModel purchaseRequestModel, int userId)
        //{
        //    var purchase = await _dbContext.Purchases.Include(p => p.Movie).Include(p => p.User)
        //        .FirstOrDefaultAsync(p => p.UserId == userId);
        //    return purchase;
        //}

        //public async Task<Purchase> PurchaseMovie(PurchaseRequestModel purchaseRequestModel, int userId)
        //{
        //    var purMovie = await _dbContext.Purchases.Include(p => p.Movie).Include(p => p.User)
        //        .ThenInclude(p => p.Id == userId).FirstOrDefaultAsync(p => p.UserId == userId);
        //    return purMovie;
        //}

        //public async Task<Purchase> GetAllPurchasesForUser(int id)
        //{
        //    var purchases = await _dbContext.Purchases.Include(p => p.Movie)
        //        .Include(p => p.User).ThenInclude(p => p.Id == id)
        //        .FirstOrDefaultAsync(p => p.UserId == id);
        //    return purchases;
        //}

        //public async Task<Purchase> GetPurchasesDetails(int userId, int movieId)
        //{
        //    var purchaseDetails = await _dbContext.Purchases.Include(p => p.Movie)
        //        .ThenInclude(p => p.Id == movieId).Include(p => p.User)
        //        .ThenInclude(p => p.Id == userId).FirstOrDefaultAsync(p => p.UserId == userId);
        //    return purchaseDetails;
        //}
    }
}
