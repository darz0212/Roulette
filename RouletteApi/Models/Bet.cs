﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RouletteApi.Models
{
    public class Bet
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string idUser { get; set; }
        public string idRoulette { get; set; }
        public int moneyValue { get; set; }
        public int betNumber { get; set; }
        public string betColor { get; set; }
        public bool isWinner { get; set; }
    }
}
