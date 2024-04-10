using MatchMateCore.Dtos.UsersViewModels;


namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IFriendshipInterface
    {
        public Task SendFriendRequestAsync(string senderId, string receiverId);
        public Task RejectFriendRequestAsync(string senderId, string receiverId);
        public Task<bool> CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(string firstUserId, string secondUserId);
        public Task RemoveFriendAsync(string firstUserId, string secondUserId);
        public Task<UserFriendshipModelList> GetPendingRequestsAsync(string receiverId,int pageNumber);
        public Task AcceptFriendRequestAsync(string senderId, string receiverId);
        public Task<bool> CheckIfThereIsARelationShipBetweenUsersAsync(string firstUserId, string secondUserId);
        public Task GetActiveFriendsAsync(string userId, UserFriendshipModelList friendshipModelList);
    }
}
