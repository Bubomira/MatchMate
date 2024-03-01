using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MatchMateInfrastructure.MongoModels
{
    public class ProfilePicture
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string Picture { get; set; } = string.Empty;
    }
}
