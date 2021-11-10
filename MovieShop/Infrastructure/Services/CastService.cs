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
    public class CastService : ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastResponseModel> GetAllCast(int id)
        {
            var cast = await _castRepository.GetCast(id);
            if (cast == null)
            {
                // throw new Exception($"No Movie Found for this {id}");
                return null;
            }

            var casts = new CastResponseModel
            {
                Id = cast.Id,
                Name = cast.Name,
                //Character = cast.Character,
                ProfilePath = cast.ProfilePath,
                Movies = cast.Movies
            };
            return casts;
        }
    }
}
