
using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class FriendshipService : IFriendshipInterface
    {
        private readonly IRepository _repository;
        public FriendshipService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task AcceptFriendRequestAsync(string senderId, string receiverId)
        {
            var friendship = await FindFriendshipAsync(senderId, receiverId);

            friendship.IsActive = true;

            await _repository.SaveChangesAsync();
        }

        public Task<bool> CheckIfThereIsARelationShipBetweenUsersAsync(string firstUserId, string secondUserId) =>
            _repository.AllReadOnly<Friendship>()
            .AnyAsync(f => (f.SenderId == firstUserId && f.ReceiverId == secondUserId)
            || (f.SenderId == secondUserId && f.ReceiverId == firstUserId));

        public Task<bool> CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(string firstUserId, string secondUserId) =>
             _repository.AllReadOnly<Friendship>()
            .AnyAsync(f => (f.SenderId == firstUserId && f.ReceiverId == secondUserId && f.IsActive)
            || (f.SenderId == secondUserId && f.ReceiverId == firstUserId && f.IsActive));

        public async Task GetActiveFriendsAsync(string userId, UserFriendshipModelList friendshipModelList)
        {
            var activeFriends = _repository.AllReadOnly<Friendship>()
              .Where(f => f.IsActive == true &&
              (f.SenderId == userId || f.ReceiverId == userId));

            friendshipModelList.TotalFriends = activeFriends.Count();

            var searchTerm = friendshipModelList.SearchItem;
            if (!String.IsNullOrEmpty(searchTerm))
            {
                searchTerm = searchTerm.ToLower();
                activeFriends = activeFriends.Where(f => f.SenderId == userId ? f.Receiver.UserName.ToLower().Contains(searchTerm) : f.Sender.UserName.ToLower().Contains(searchTerm));
            }

            friendshipModelList.Friends = await activeFriends.Skip(UserFriendshipModelList.friendsOnPage * (friendshipModelList.PageNumber - 1))
             .Take(UserFriendshipModelList.friendsOnPage)
            .Select(f => new UserCardModel()
            {
                IsActiveFriendship = true,
                UserId = f.SenderId == userId ? f.ReceiverId : f.SenderId,
                Bio = f.SenderId == userId ? f.Receiver.Bio : f.Sender.Bio,
                Username = f.SenderId == userId ? f.Receiver.UserName : f.Sender.UserName,
                Interests = f.SenderId == userId ?
                    f.Receiver.UsersInterests.Select(ui => ui.Interest.Name).ToList()
                    :
                    f.Sender.UsersInterests.Select(ui => ui.Interest.Name).ToList()
            })
           .ToListAsync();

        }

        public async Task RejectFriendRequestAsync(string senderId, string receiverId)
        {
            var friendship = await FindFriendshipAsync(senderId, receiverId);

            await _repository.Remove<Friendship>(friendship);

            await _repository.SaveChangesAsync();
        }

        public async Task SendFriendRequestAsync(string senderId, string receiverId)
        {
            await _repository.AddAsync<Friendship>(new Friendship()
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                IsActive = false,
            });

            await _repository.SaveChangesAsync();
        }
        public async Task<UserFriendshipModelList> GetPendingRequestsAsync(string receiverId, int pageNumber)
        {
            UserFriendshipModelList model = new UserFriendshipModelList();

            var pendingRequests = _repository.AllReadOnly<Friendship>()
            .Where(f => f.ReceiverId == receiverId && f.IsActive == false);

            model.TotalFriends = pendingRequests.Count();

            model.Friends = await pendingRequests.Skip(12 * pageNumber)
            .Take(12)
            .Select(f => new UserCardModel()
            {
                IsPendingFriendship = true,
                UserId = f.Sender.Id,
                Bio = f.Sender.Bio,
                Username = f.Sender.UserName,
                Interests = f.Sender.UsersInterests.Select(ui => ui.Interest.Name).ToList(),
            })
            .ToListAsync();

            return model;
        }



        private Task<Friendship?> FindFriendshipAsync(string senderId, string receiverId) =>
            _repository.All<Friendship>()
            .FirstOrDefaultAsync(f => f.SenderId == senderId && f.ReceiverId == receiverId);

        public async Task RemoveFriendAsync(string firstUserId, string secondUserId)
        {
            var friendship = await _repository.All<Friendship>().
                Where(f => (f.SenderId == firstUserId && f.ReceiverId == secondUserId) ||
                (f.SenderId == secondUserId && f.ReceiverId == firstUserId))
                .FirstOrDefaultAsync();

            await _repository.Remove<Friendship>(friendship);

            await _repository.SaveChangesAsync();
        }
    }
}
