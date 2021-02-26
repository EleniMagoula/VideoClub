using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;

        public BookingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();
        }

        public async Task Update() //update booking
        {
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Booking> GetActiveBookings()
        {
            return _context.Bookings
                .Where(b => b.DateOfReturn == null)
                .Include(b => b.DVD.Movie)
                .Include(b => b.Customer)
                .AsNoTracking();
        }

        public IEnumerable<Booking> GetCustomerBookings(string customerId)
        {
            return _context.Bookings
                .Where(b => b.CustomerId == customerId)
                .AsNoTracking();
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return _context.Bookings
                .Include(b => b.Customer);
        }

        public async Task<Booking> FindById(int id)
        {
            return await _context.Bookings
                .Where(b => b.Id == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }

        public async Task<Booking> FindByIdWithDVD(int id)
        {
            return await _context.Bookings
                .Where(b => b.Id == id)
                .Include(b => b.DVD)
                .SingleOrDefaultAsync();
        }

        public IEnumerable<Booking> GetActiveBookingsByCustomerId(string customerId)
        {
            return _context.Bookings
                .Where(b => b.CustomerId == customerId && b.DateOfReturn == null)
                .AsNoTracking();
        }
    }

}
