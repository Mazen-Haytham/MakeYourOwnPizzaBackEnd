using System.Collections.Generic;
using System.Threading.Tasks;

namespace MakeYourOwnPizza.Application.Drivers
{
    public interface IDriverService
    {
        Task<IEnumerable<DriverResponse>> GetAllDriversAsync();
    }
}
