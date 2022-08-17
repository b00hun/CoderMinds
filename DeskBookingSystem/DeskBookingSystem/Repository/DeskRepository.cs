using DeskBookingSystem.Data;
using DeskBookingSystem.Models;
using DeskBookingSystem.Repository.IRepository;

namespace DeskBookingSystem.Repository
{
    public class DeskRepository : Repository<Desk>, IDeskRepository
    {
        private readonly ApplicationDbContext _context;
        public DeskRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
    }
}
