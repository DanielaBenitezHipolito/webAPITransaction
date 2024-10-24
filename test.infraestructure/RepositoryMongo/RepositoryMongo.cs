using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Nest;
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
        public async Task<T> GetByIdAsync(string id)
        {
            var objectId = ObjectId.Parse(id);
            var filter = Builders<T>.Filter.Eq("_id", objectId);
            var result = await _collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        public async Task<IEnumerable<T>> GetByStatusAsync(string status)
        {
            var filter = Builders<T>.Filter.Eq("Status", status);
            var result = await _collection.Find(filter).ToListAsync();
            return result;
        }
    }

}
