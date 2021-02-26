using System.Collections.Generic;
using System.Threading.Tasks;
using VideoClub.Core.Entities;

namespace VideoClub.Core.Interfaces
{
    public interface ICustomerService
    {
        Task<ApplicationUser> GetCustomerById(string id);
        Task<IEnumerable<ApplicationUser>> GetAllCustomers();
    }
}
