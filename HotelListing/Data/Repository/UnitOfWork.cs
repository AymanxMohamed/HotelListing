using HotelListing.Data.IRepository;
using System.Threading.Tasks;

namespace HotelListing.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        private readonly IRepository<Country> _countries;

        private readonly IRepository<Hotel> _hotels;

        public IRepository<Country> Countries => _countries ?? new Repository<Country>(_context);

        public IRepository<Hotel> Hotels => _hotels ?? new Repository<Hotel>(_context);

        public UnitOfWork(DatabaseContext context) => _context = context;

        public async Task Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
           _context.Dispose();
            System.GC.SuppressFinalize(this);
        }
    }
}
