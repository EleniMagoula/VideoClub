using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoClub.Core.Constants;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Services;
using VideoClub.Web.Areas.Bookings.Models;

namespace VideoClub.Web.Areas.Bookings.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingDb;
        private readonly IDVDService _dvdDb;
        private readonly ICustomerService _customerDb;
        private readonly IMovieService _movieDb;
        private readonly IMapper _mapper;
        private readonly ILoggingService _logger;

        public BookingController(IBookingService bookingDb, IDVDService dvdDb, ICustomerService customerDb, 
            IMovieService movieDb, IMapper mapper, ILoggingService logger)
        {
            _bookingDb = bookingDb;
            _dvdDb = dvdDb;
            _customerDb = customerDb;
            _movieDb = movieDb;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ActionResult> CustomerBookings(string customerId)
        {
            var customer = await _customerDb.GetCustomerById(customerId);

            if (customer == null)
                return HttpNotFound();

            var bookings = _bookingDb.GetCustomerBookings(customerId);

            var bookingsList = new BookingsPerCustomerViewModel(customerId, customer, bookings);

            return View(bookingsList);
        }

        public ActionResult ActiveBookings()
        {
            var bookings = _bookingDb.GetActiveBookings();

            var activeBookings = new BookingListViewModel(bookings);

            _logger.Writer.Information("You just viewed active bookings");

            return View(activeBookings);
        }

        public async Task<ActionResult> CreateFromCustomerList(string customerId)
        {
            var customer = await _customerDb.GetCustomerById(customerId);

            if (customer == null)
                return HttpNotFound();

            var availableDVDs = _dvdDb.GetAvailableDVDs();

            var bookingForm = new BookingBindingModel(customerId, availableDVDs);

            return View(bookingForm);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFromCustomerList(BookingBindingModel bookingForm)
        {
            // dvds group by title -- to send back in case of failed create
            var availableDVDs = _dvdDb.GetAvailableDVDs();

            if (!ModelState.IsValid)
            {
                bookingForm.DVDs = availableDVDs;

                return View(bookingForm);
            }

            //var booking = new Booking(bookingForm.CustomerId, bookingForm.DVDId, bookingForm.Comments);
            var booking = _mapper.Map<Booking>(bookingForm);

            var dvdToBook = await _dvdDb.FindDVDById(bookingForm.DVDId);

            booking.ChangeBookedDVDStatus(dvdToBook);

            await _bookingDb.Add(booking);

            _logger.Writer.Information("A booking was created for {userId} and {movieTitle}", booking.CustomerId, booking.DVD.Movie.Title);

            return RedirectToAction("CustomerList", "Customer", new { area = "Customers" });
        }

        public async Task<ActionResult> CreateFromMovieList(int movieId)
        {
            var movie = await _movieDb.GetMovieById(movieId);

            if (movie == null)
                return HttpNotFound();

            var dvdId = await _dvdDb.GetAvailableDVD(movieId);
            var customers = await _customerDb.GetAllCustomers();

            var bookingForm = new BookingBindingModel(dvdId, customers);

            return View(bookingForm);
        }

        [HttpPost]
        public async Task<ActionResult> CreateFromMovieList(BookingBindingModel bookingForm)
        {
            var customers = await _customerDb.GetAllCustomers();

            if (!ModelState.IsValid)
            {
                bookingForm.Customers = customers;

                return View(bookingForm);
            }

            //var booking = new Booking(bookingForm.CustomerId, bookingForm.DVDId, bookingForm.Comments);
            var booking = _mapper.Map<Booking>(bookingForm);

            var bookedDVD = await _dvdDb.FindDVDById(bookingForm.DVDId);

            if (bookedDVD == null)
                return HttpNotFound();

            booking.ChangeBookedDVDStatus(bookedDVD);

            await _bookingDb.Add(booking);

            return RedirectToAction("Index", "Movie", new { area = "Movies" });
        }

        public ActionResult AllBookings()
        {
            var bookings = _bookingDb.GetAllBookings();
            var bookingList = new BookingListViewModel(bookings);

            return View(bookingList);
        }

        public async Task<ActionResult> Return(int bookingId)
        {
            var booking = await _bookingDb.FindById(bookingId);
            //var bindingModel = new BookingBindingModel(booking.CustomerId, booking.DVDId, booking.Comments)
            //{
            //    Id = booking.Id
            //};

            var bindingModel = _mapper.Map<BookingBindingModel>(booking);

            return View(bindingModel);
        }

        [HttpPost]
        public async Task<ActionResult> Returned(int bookingId)
        {
            var booking = await _bookingDb.FindByIdWithDVD(bookingId);

            booking.Returned();

            await _bookingDb.Update();

            return RedirectToAction("ActiveBookings");
        }
    }

}