

using MatchMateCore.Dtos.UsersViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IUserInterface
    {
        public Task<List<UserCardModel>> GetUsersWithTheSameInterests(string userId,int pageCount);
    }
}
