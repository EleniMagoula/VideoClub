using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface IDVDService
    {
        IEnumerable<DVD> GetAvailableDVDs();
        Task<DVD> FindDVDById(int dvdId);
        Task<int> GetAvailableDVD(int movieId);
    }
}
