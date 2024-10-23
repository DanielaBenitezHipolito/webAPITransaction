using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using test.application.Settings;
using test.domain.interfaces;

namespace test.infraestructure.NewFolder1
{
    public class RepositorySQL<T> : IRepositorySQL<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;


        //public RepositorySQL(IOptions<MongoSettings> settings)
        //{
        //    var client = new MongoClient(settings.Value.ConnectionString);
        //    var database = client.GetDatabase(settings.Value.Database);
        //    _collection = database.GetCollection<T>(typeof(T).Name);
        //}
        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
