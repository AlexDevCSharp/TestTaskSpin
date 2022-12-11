using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace TestTaskSpin.Models
{
    public class PlayerBalance
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string PlayerId { get; set; }

        public int CurrentBalance { get; set; }

        public PlayerBalance(string playerId, int currentBalance)
        {
            PlayerId = playerId;
            CurrentBalance = currentBalance;
        }
    }
}
