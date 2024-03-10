

namespace MatchMateCore.Interfaces.MongoInterfaces
{
    public interface IProfilePictureInterface
    {
        public Task ChangeProfilePictureMongoAsync(string userId, string file);
        public Task SaveProfilePictureToMongoAsync(string userId,string file);
        public Task<string> GetProfilePictureFromMongoAsync(string userId);
    }
}
