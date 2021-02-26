using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Core.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public ApplicationUser Customer { get; set; }
        public int DVDId { get; set; }
        public DVD DVD { get; set; }
        public DateTime DayOfBooking { get; private set; } // DateTime.Now.Date > day of creation
        public DateTime ToBeReturned { get; private set; } // in a week
        public DateTime? DateOfReturn { get; set; }
        public string Comments { get; set; }

        public Booking()
        { }

        public Booking(string customerId, int dvdId, string comments)
        {
            DayOfBooking = DateTime.UtcNow;
            ToBeReturned = DayOfBooking.AddDays(7);
            CustomerId = customerId;
            DVDId = dvdId;
            Comments = comments;
        }

        public void ChangeBookedDVDStatus(DVD DVD)
        {
            DVD.IsAvailable = false;
        }

        public void Returned()
        {
            DateOfReturn = DateTime.UtcNow;
            DVD.MakeAvailable();
        }
    }

}
