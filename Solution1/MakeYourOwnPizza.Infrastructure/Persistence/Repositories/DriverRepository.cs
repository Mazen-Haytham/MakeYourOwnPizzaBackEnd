using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MakeYourOwnPizza.Application.Abstractions.Persistence;
using MakeYourOwnPizza.Application.Drivers;
using MakeYourOwnPizza.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MakeYourOwnPizza.Infrastructure.Persistence.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private readonly AppDbContext _context;

        public DriverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DriverResponse>> GetAllDriversAsync()
        {
            var deliveryUsers = await _context.User
                .Where(u => u.role == Domain.Enums.Role.Delivery)
                .ToListAsync();

            foreach (var user in deliveryUsers)
            {
                var exists = await _context.Driver.AnyAsync(d => d.UserId == user.Id);
                if (!exists)
                {
                    _context.Driver.Add(new Driver
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        Zone = "Cairo",
                        Status = Domain.Enums.DriverStatus.Available
                    });
                }
            }
            await _context.SaveChangesAsync();

            return await _context.Driver
                .Include(d => d.User)
                .Select(d => new DriverResponse
                {
                    DriverId = d.Id.ToString(),
                    DriverName = (d.User.firstName + " " + d.User.lastName).Trim(),
                    DriverZone = d.Zone,
                    DriverPhone = d.User.phone,
                    DriverStatus = d.Status.ToString()
                })
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
