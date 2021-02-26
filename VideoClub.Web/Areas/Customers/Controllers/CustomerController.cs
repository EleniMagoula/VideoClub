using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using VideoClub.Core.Constants;
using VideoClub.Core.Entities;
using VideoClub.Core.Interfaces;
using VideoClub.Infrastructure.Data;
using VideoClub.Web.Areas.Customers.Models;

namespace VideoClub.Web.Areas.Customers.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerDb;
        private readonly IBookingService _bookingDb;

        public CustomerController(ICustomerService customerDb, IBookingService bookingDb)
        {
            _customerDb = customerDb;
            _bookingDb = bookingDb;
        }

        public async Task<ActionResult> CustomerList()
        {
            var customers = await _customerDb.GetAllCustomers();

            foreach (var customer in customers)
                customer.ActiveBookings = _bookingDb.GetActiveBookingsByCustomerId(customer.Id);

            var viewModel = new CustomerListViewModel(customers);

            return View(viewModel);
        }
    }

}