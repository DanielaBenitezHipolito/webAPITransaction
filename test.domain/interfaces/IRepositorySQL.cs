using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.domain.interfaces
{
    public interface IRepositorySQL<T> where T : class
    {
        Task AddAsync(T entity);

    }

}
