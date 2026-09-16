using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Drivers;

namespace MakeYourOwnPizza.Application.Abstractions.Persistence
{
    public interface IDriverRepository
    {
        Task<IEnumerable<DriverResponse>> GetAllDriversAsync();
    }
}
