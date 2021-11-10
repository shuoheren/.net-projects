using ApplicationCore.Entities;
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
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Include(p => p.Id).Include(p => p.PurchaseDateTime)
                .Include(p => p.PurchaseNumber).Include(p => p.MovieId).Include(p => p.Movie)
                .Include(p => p.TotalPrice).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Include(m => m.Movie).Where(p => p.UserId == userId).ToListAsync();
            //var user = await _dbContext.Purchases.Include(p => p.UserId == userId).Include(p => p.User).ThenInclude(p => p.Purchases)
            //    .Include(p => p.PurchaseDateTime).Include(p => p.PurchaseNumber).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 1)
        {
            var movie = await _dbContext.Purchases.Include(p => p.UserId).Include(p => p.User).ThenInclude(p => p.Purchases)
                .Include(p => p.PurchaseDateTime).Include(p => p.PurchaseNumber).Include(p => p.MovieId == movieId).Include(p => p.Movie).ToListAsync();
            return movie;
        }

        public async Task<Purchase> GetPurchaseDetails(int userId, int movieId)
        {
            var purDetails = await _dbContext.Purchases.FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);
            return purDetails;
        }
    }
}
