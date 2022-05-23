using IList.Data.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IList.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext _databaseContext;

        private readonly DbSet<T> _dbSet;

        public Repository(DatabaseContext context)
        {
            _databaseContext = context;
            _dbSet = context.Set<T>();
        }

        public async Task Delete(int id) => _dbSet.Remove(await _dbSet.FindAsync(id));
        
        public void DeleteRange(IEnumerable<T> entities) => _dbSet.RemoveRange(entities);

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> values = _dbSet;

            if (includes != null)
                includes.ForEach(includeProperty => values = values.Include(includeProperty));

            return await values.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> GetAll(
            Expression<Func<T, bool>> expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<string> includes = null)
        {
            IQueryable<T> values = _dbSet;

            if (expression != null)
                values = values.Where(expression);

            if (includes != null)
                includes.ForEach(includeProperty => values = values.Include(includeProperty));

            if (orderBy != null)
                values = orderBy(values);

            return await values.AsNoTracking().ToListAsync();
        }

        public async Task Insert(T entity) => await _dbSet.AddAsync(entity);

        public async Task InsertRange(IEnumerable<T> entities) => await _dbSet.AddRangeAsync(entities);

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            // This to make the state of the entity modified after updating it
            _databaseContext.Entry(entity).State = EntityState.Modified;
        }

    }
}
