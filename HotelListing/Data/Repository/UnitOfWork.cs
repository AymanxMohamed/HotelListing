using HotelListing.Data.IRepository;
using System.Threading.Tasks;

namespace HotelListing.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        public IRepository<Country> Countries;

        public IRepository<Hotel> Hotels;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            Countries = new Repository<Country>(_context);
            Hotels = new Repository<Hotel>(_context);
        }




        public async Task Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
           _context.Dispose();
            System.GC.SuppressFinalize(this);
        }
    }
}
