using System;
using System.Threading.Tasks;

namespace HotelListing.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Country> Countries { get; }

        IRepository<Hotel> Hotels { get; }

        Task Save();
    }
}
