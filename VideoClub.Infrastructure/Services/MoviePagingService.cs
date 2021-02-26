using System;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Infrastructure.Data;
using VideoClub.Infrastructure.Models;
using VideoClub.Infrastructure.Models.Dtos;
using System.Data.Entity;

namespace VideoClub.Infrastructure.Services
{
    public class MoviePagingService : IMoviePagingService
    {
        private readonly ApplicationDbContext _context;

        public MoviePagingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationModel<Movie>> GetPaginatedMovies(PaginationDto paginationDto, string movieGenre, string title)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.DVDs)
                .OrderBy(m => m.Id)
                .AsNoTracking()
                .AsQueryable();

            if (!string.IsNullOrEmpty(movieGenre) && !string.IsNullOrEmpty(title))
                moviesQuery = moviesQuery
                    .Where(m => m.Genre.ToString() == movieGenre && m.Title.Contains(title));
            else if (!string.IsNullOrEmpty(movieGenre))
                moviesQuery = moviesQuery
                    .Where(m => m.Genre.ToString() == movieGenre);
            else if (!string.IsNullOrEmpty(title))
                moviesQuery = moviesQuery
                    .Where(m => m.Title.Contains(title));

            var moviesCount = await moviesQuery
                .CountAsync();

            var toSkip = (paginationDto.CurrentPage - 1) * paginationDto.PageSize;

            moviesQuery = moviesQuery
                    .Skip(toSkip)
                    .Take(paginationDto.PageSize);

            var movies = await moviesQuery
                .ToListAsync();

            var totalPagesCount = (int)Math.Ceiling((double)moviesCount / paginationDto.PageSize);

            return new PaginationModel<Movie>(movies, paginationDto.CurrentPage, paginationDto.PageSize, moviesCount, totalPagesCount);
        }

    }
}
