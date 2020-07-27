using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models
{
    public class UserRoulette
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string name { get; set; }
        public int credit { get; set; }
    }
}
