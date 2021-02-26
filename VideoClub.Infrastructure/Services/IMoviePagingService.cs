using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Infrastructure.Models;
using VideoClub.Infrastructure.Models.Dtos;

namespace VideoClub.Infrastructure.Services
{
    public interface IMoviePagingService
    {
        Task<PaginationModel<Movie>> GetPaginatedMovies(PaginationDto paginationDto, string movieGenre, string title);
    }
}
