using MongoDB.Bson;
using MongoDB.Driver;
using RouletteApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApi.Repositories
{
    public class RouletteCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Roulette> collection;

        public RouletteCollection()
        {
            collection = _repository.db.GetCollection<Roulette>("Roulettes");
        }

        public List<Roulette> GetAllRoulettes(bool isActive = false)
        {
            if (isActive)
                return collection.Find(x => x.state != "close").ToList();

            return collection.Find(new BsonDocument()).ToList();
        }

        public  Roulette GetRouletteById(string id, bool isActive = false)
        {
            if(isActive)
                return collection.Find(x => x.id == id && x.state != "close").FirstOrDefault();

            return collection.Find(x => x.id == id).FirstOrDefault();
        }

        public string InsertRoulette(Roulette roulette)
        {
            collection.InsertOne(roulette);
            return roulette.id;
        }

        public string UpdateRoulette(Roulette roulette, string id)
        {
            var filter = Builders<Roulette>.Filter.Eq(x => x.id, id);
            roulette.id = id;
            collection.ReplaceOne(filter, roulette);
            return roulette.id;
        }
    }
}
