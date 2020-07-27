using MongoDB.Bson;
using MongoDB.Driver;
using RouletteApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace RouletteApi.Repositories
{
    public class UserRouletteCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<UserRoulette> collection;

        public UserRouletteCollection()
        {
            collection = _repository.db.GetCollection<UserRoulette>("Users");
        }

        public List<UserRoulette> GetAllUsers()
        {
            return collection.Find(new BsonDocument()).ToList();
        }

        public UserRoulette GetUsersById(string id)
        {
            var result = collection.Find(x => x.id == id).FirstOrDefault();
            return result;
        }

        public string InsertUsers(UserRoulette user)
        {
            collection.InsertOne(user);
            return user.id;
        }

        public string UpdateUsers(UserRoulette user, string id)
        {
            var filter = Builders<UserRoulette>.Filter.Eq(x => x.id, id);
            user.id = id;
            collection.ReplaceOne(filter, user);
            return user.id;
        }
    }
}
