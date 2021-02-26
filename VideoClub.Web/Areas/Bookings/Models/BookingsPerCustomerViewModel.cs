using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Bookings.Models
{
    public class BookingsPerCustomerViewModel
    {
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }

        public BookingsPerCustomerViewModel(string customerId, ApplicationUser customer, IEnumerable<Booking> bookings)
        {
            CustomerId = customerId;
            Customer = customer;
            Bookings = bookings;
        }
    }
}