using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Nest;
using test.application.Settings;
using test.domain.interfaces;
using test.infraestructure.DBContext;

namespace test.infraestructure.RepositorySQL
{
    public class RepositorySQL<T> : IRepositorySQL<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;


        public RepositorySQL(SqlDBContext context)
        {
            _context = context;

            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);

                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
            }
            
        }
        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        
    }
}
