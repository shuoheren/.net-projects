using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IGenreRepository : IAsyncRepository<Genre>
    {
        Task<IEnumerable<Genre>> GetGenres();
        //Task<IEnumerable<Movie>> GetGenres();
        //Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageIndex = 1);
    }
}
