using System.Collections.Generic;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;

namespace MakeYourOwnPizza.Application.Drivers
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;

        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public async Task<IEnumerable<DriverResponse>> GetAllDriversAsync()
        {
            return await _driverRepository.GetAllDriversAsync();
        }
    }
}
