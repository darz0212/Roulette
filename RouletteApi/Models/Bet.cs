using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models
{
    public class Bet
    {
        [BsonId]
        public ObjectId id { get; set; }
        public int moneyValue { get; set; }
        public int betNumber { get; set; }
        public string betColor { get; set; }
    }
}
