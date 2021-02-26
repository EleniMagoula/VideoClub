using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Bookings.Models
{
    public class BookingListViewModel
    {
        public IEnumerable<Booking> Bookings { get; set; }

        public BookingListViewModel(IEnumerable<Booking> bookings)
        {
            Bookings = bookings;
        }
    }
}