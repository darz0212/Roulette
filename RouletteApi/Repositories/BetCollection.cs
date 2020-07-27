using MongoDB.Bson;
using MongoDB.Driver;
using RouletteApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApi.Repositories
{
    public class BetCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Bet> collection;

        public BetCollection()
        {
            collection = _repository.db.GetCollection<Bet>("Bet");
        }

        public List<Bet> GetAllBets()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public Bet GetBetById(string id)
        {
            var result = collection.Find(x => x.id == id).FirstOrDefault();
            return result;
        }

        public string InsertBet(Bet bet)
        {
            collection.InsertOne(bet);
            return bet.id;
        }

        public string UpdateBet(Bet bet, string id)
        {
            var filter = Builders<Bet>.Filter.Eq(x => x.id, id);
            bet.id = id;
            collection.ReplaceOne(filter, bet);
            return bet.id;
        }
    }
}
