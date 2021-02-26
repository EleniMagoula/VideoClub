using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;

namespace VideoClub.Common.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> GetAllCustomers()
        {
            return await _context.Users
                .Include(u => u.Bookings)
                .Where(u => u.Roles.Count < 1)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<ApplicationUser> GetCustomerById(string id)
        {
            return await _context.Users
                .Where(c => c.Id == id)
                .AsNoTracking()
                .SingleOrDefaultAsync();
        }
    }

}
