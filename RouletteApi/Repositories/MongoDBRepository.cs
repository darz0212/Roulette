using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RouletteApi.Repositories
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase db;
        public MongoDBRepository()
        {
            client = new MongoClient("mongodb+srv://admin:Prueba1234@cluster0.sbd8o.mongodb.net/roulette_test?retryWrites=true&w=majority");
            db = client.GetDatabase("roulette_test");
        }
    }
}
