using IList.Data.IRepository;
using System;
using System.Threading.Tasks;

namespace IList.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;
        private IRepository<Country> _countries;
        private IRepository<Hotel> _hotels;

        public UnitOfWork(DatabaseContext context) => _context = context;

        public IRepository<Country> Countries => _countries ??= new Repository<Country>(_context);
        public IRepository<Hotel> Hotels => _hotels ??= new Repository<Hotel>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
