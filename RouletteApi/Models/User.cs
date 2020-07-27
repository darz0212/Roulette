using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models
{
    public class User
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string name { get; set; }
        public int credit { get; set; }
    }
}
