using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;
using VideoClub.Infrastructure.Models;
using VideoClub.Infrastructure.Models.Dtos;

namespace VideoClub.Common.Services
{
    public class MovieService : IMovieService
    {
        private readonly ApplicationDbContext _context;

        public MovieService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Movie movie, int availableDVDs)
        {

            using (var dbTran = _context.Database.BeginTransaction()) // Transaction example 
            {
                try
                {
                    for (int i = 0; i < availableDVDs; i++)
                    {
                        var dvd = new DVD(movie);
                        movie.DVDs.Add(dvd);
                    }

                    _context.Movies.Add(movie);
                    await _context.SaveChangesAsync();

                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback(); // if something goes wrong, it rollbacks any already occured changes before the exception
                }
            }
        }

        public async Task<Movie> GetMovieById(int id)
        {
            return await _context.Movies
                .Where(m => m.Id == id)
                .Include(m => m.DVDs)
                .SingleOrDefaultAsync();
        }
    }
}
