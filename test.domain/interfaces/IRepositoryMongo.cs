using LinqToTwitter.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.domain.interfaces
{
    public interface IRepositoryMongo<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetByStatusAsync(string status);
        Task AddAsync(T entity);
        Task UpdateAsync(string id, T entity);
    }   
}
