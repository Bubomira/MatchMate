using MatchMateCore.Interfaces.MongoInterfaces;
using MatchMateInfrastructure.MongoModels;
using MongoDB.Driver;

using static MatchMateInfrastructure.Mongo.MatchMateMongoDatabaseSettings;

namespace MatchMateCore.Services.MongoServices
{
    public class ProfilePictureService : IProfilePictureInterface
    {
        private readonly IMongoCollection<ProfilePicture> _profilePictureCollection;
        public ProfilePictureService()
        {
            var mongoClient = new MongoClient(ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(DatabaseName);

            _profilePictureCollection = mongoDatabase.GetCollection<ProfilePicture>(CollectionName);
        }
        public async Task ChangeProfilePictureMongoAsync(string userId, string file)
        {
            var profilePictureObj = Builders<ProfilePicture>.Filter.Eq(p => p.UserId, userId);

            var update = Builders<ProfilePicture>.Update.Set(profilePicture => profilePicture.Picture, file);

            await _profilePictureCollection.UpdateOneAsync(profilePictureObj, update);

        }
        public async Task<string> GetProfilePictureFromMongoAsync(string userId)
        {
            var profilePicture = await
               (await _profilePictureCollection.FindAsync(pp => pp.UserId == userId))
               .FirstOrDefaultAsync();

            return profilePicture?.Picture;
        }

        public async Task SaveProfilePictureToMongoAsync(string userId, string file)
        {
            ProfilePicture profilePicture = new ProfilePicture()
            {
                UserId = userId,
                Picture = file,
            };
            await _profilePictureCollection.InsertOneAsync(profilePicture);
        }
    }
}
