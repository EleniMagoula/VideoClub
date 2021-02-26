using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoClub.Core.Entities;

namespace VideoClub.Web.Areas.Customers.Models
{
    public class CustomerListViewModel
    {
        public IEnumerable<ApplicationUser> Customers { get; set; }

        public CustomerListViewModel(IEnumerable<ApplicationUser> customers)
        {
            Customers = customers;
        }
    }
}