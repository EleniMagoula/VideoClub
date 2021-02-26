using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<Booking> GetCustomerBookings(string customerId);
        IEnumerable<Booking> GetActiveBookings();
        IEnumerable<Booking> GetActiveBookingsByCustomerId(string customerId);
        IEnumerable<Booking> GetAllBookings();
        Task<Booking> FindById(int id);
        Task<Booking> FindByIdWithDVD(int id);
        Task Add(Booking booking);
        Task Update();
    }
}
