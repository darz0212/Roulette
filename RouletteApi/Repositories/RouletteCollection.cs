using MongoDB.Bson;
using MongoDB.Driver;
using RouletteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Repositories
{
    public class RouletteCollection : IRouletteCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Roulette> collection;

        public RouletteCollection()
        {
            collection = _repository.db.GetCollection<Roulette>("Roulettes");
        }

        public async Task<List<Roulette>> GetAllRoulettes()
        {
            return await collection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Roulette> GetRouletteById(string id)
        {
            return await collection.FindAsync(new BsonDocument { { "id", new ObjectId(id)} }).Result.FirstAsync();
        }

        public async Task InsertRoulette(Roulette roulette)
        {
            await collection.InsertOneAsync(roulette);
        }

        public async Task UpdateRoulette(Roulette roulette, string id)
        {
            var filter = Builders<Roulette>.Filter.Eq(x => x.id, roulette.id);
            roulette.id = id;
            await collection.ReplaceOneAsync(filter, roulette);
        }
    }
}
