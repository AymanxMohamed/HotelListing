using IList.Data.IRepository;
using System;
using System.Threading.Tasks;

namespace IList.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private readonly IRepository<Country> _countries;
        private readonly IRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
            _countries = new Repository<Country>(_context);
            _hotels = new Repository<Hotel>(_context);
        }

        public async Task Save() => await _context.SaveChangesAsync();

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public IRepository<Country> Countries => _countries;

        public IRepository<Hotel> Hotels => _hotels;

    }
}
