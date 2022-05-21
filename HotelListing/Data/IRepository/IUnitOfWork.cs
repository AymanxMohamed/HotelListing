using System;
using System.Threading.Tasks;

namespace IList.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Country> Countries { get; }

        IRepository<Hotel> Hotels { get; }

        Task Save();
    }
}
