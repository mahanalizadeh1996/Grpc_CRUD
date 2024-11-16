using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TestRira.Data;

namespace TestRira.Repo
{
    public class EntityRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly CustomerContext _context;
        private DbSet<TEntity> table = null;

        public EntityRepository(CustomerContext dataProvider)
        {
            _context = dataProvider;
            table = _context.Set<TEntity>();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {
            return await table.FindAsync(Id);
        }


        public async Task UpdateAsync(TEntity entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();

        }
        public async Task RemoveAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

        }
    }
}
