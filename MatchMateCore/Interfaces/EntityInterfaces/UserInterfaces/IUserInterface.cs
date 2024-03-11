
using MatchMateCore.Dtos.UsersViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IUserInterface
    {
        public Task<UserProfileModel> GetCurrentUserInfo(string userId);
        public Task<List<UserCardModel>> GetUsersWithTheSameInterests(string userId,int pageCount);
        public Task AddUserBio(string userBio,string userId);
        public Task<bool> CheckIfUserHasBio(string userId);
    }
}
