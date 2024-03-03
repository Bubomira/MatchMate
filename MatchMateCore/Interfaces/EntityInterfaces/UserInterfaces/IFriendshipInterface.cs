using MatchMateCore.Dtos.UsersViewModels;


namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IFriendshipInterface
    {
        public Task SendFriendRequestAsync(string senderId, string receiverId);
        public Task RejectFriendRequestAsync(string senderId, string receiverId);
        public Task<List<UserCardModel>> ViewAllPendingRequestAsync(string receiverId);
        public Task AcceptFriendRequestAsync(string senderId, string receiverId);

        public Task<List<UserCardModel>> GetAllFriendsAsync(string userId);
    }
}
