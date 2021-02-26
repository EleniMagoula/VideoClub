using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class DVDService : IDVDService
    {
        private readonly ApplicationDbContext _context;

        public DVDService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DVD> FindDVDById(int dvdId)
        {
            return await _context.DVDs
                .Where(dvd => dvd.Id == dvdId)
                .SingleOrDefaultAsync();
        }

        public IEnumerable<DVD> GetAvailableDVDs()
        {
            return _context.DVDs
                .Where(dvd => dvd.IsAvailable)
                .Include(dvd => dvd.Movie)
                .DistinctBy(dvd => dvd.MovieId)
                .ToList();
        }

        public async Task<int> GetAvailableDVD(int movieId)
        {
            return await _context.DVDs
                .Where(dvd => dvd.IsAvailable && dvd.MovieId == movieId)
                .Include(dvd => dvd.Movie)
                .Select(dvd => dvd.Id)
                .FirstOrDefaultAsync();
        }
    }
}
