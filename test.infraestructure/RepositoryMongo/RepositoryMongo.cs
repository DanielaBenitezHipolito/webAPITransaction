using LinqToTwitter.Common.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using test.application.Settings;
using test.domain.interfaces;

namespace test.infraestructure.RepositoryMongo
{
    public class RepositoryMongo<T> : IRepositoryMongo<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;
        public RepositoryMongo(IOptions<MongoSettings> settings) {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.Database);
            _collection = database.GetCollection<T>(typeof(T).Name);
        }
        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }
        public async Task UpdateAsync(string id, T entity){
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            await _collection.ReplaceOneAsync(filter, entity);
        }
        
        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public object Find(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }

}
