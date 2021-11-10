using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface ICastService
    {
        Task<CastResponseModel> GetAllCast(int id);

        // cast name, hi/her gender, profile path, list of movies he/she is in (movie cards)
        //Task<> GetCastDetails(int id)

    }
}
