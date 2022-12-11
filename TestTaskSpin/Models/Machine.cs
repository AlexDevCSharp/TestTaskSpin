using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestTaskSpin.Models
{
    public class Machine
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        
        public int ReelLength { get; set; }

        public Machine(int reelLength)
        {
            ReelLength = reelLength;
        }
    }
}
