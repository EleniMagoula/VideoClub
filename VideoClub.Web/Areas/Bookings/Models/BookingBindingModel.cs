using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Bookings.Models
{
    public class BookingBindingModel
    {
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public ApplicationUser Customer { get; set; }

        [Required]
        public int DVDId { get; set; }

        [StringLength(200)]
        public string Comments { get; set; }

        // Available DVDs
        public IEnumerable<DVD> DVDs { get; set; }

        public IEnumerable<ApplicationUser> Customers { get; set; }

        // ctor to create
        public BookingBindingModel(string customerId, int dvdId, string comments)
        {
            CustomerId = customerId;
            DVDId = dvdId;
            Comments = comments;
        }

        // ctor to GET from Customer List
        public BookingBindingModel(string customerId, IEnumerable<DVD> dvds)
        {
            CustomerId = customerId;
            DVDs = dvds;
        }

        // ctor to GET from Movie List
        public BookingBindingModel(int dvdId, IEnumerable<ApplicationUser> customers)
        {
            DVDId = dvdId;
            Customers = customers;
        }

        public BookingBindingModel()
        { }
    }
}