
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

        public Task<bool> CheckIfThereIsARelationShipBetweenUsers(string firstUserId, string secondUserId) =>
            _repository.AllReadOnly<Friendship>()
            .AnyAsync(f => (f.SenderId == firstUserId && f.ReceiverId == secondUserId)
            || (f.SenderId==secondUserId && f.ReceiverId==firstUserId));

        public Task<List<UserCardModel>> GetAllFriendsAsync(string userId, int page) =>
             _repository.AllReadOnly<Friendship>()
             .Where(f => f.IsActive == true &&
             (f.SenderId == userId || f.ReceiverId == userId))
              .Skip(15 * page)
              .Take(15)
             .Select(f => new UserCardModel()
             {
                 UserId = f.SenderId == userId ? f.ReceiverId : f.SenderId,
                 Bio = f.SenderId == userId ? f.Receiver.Bio : f.Sender.Bio,
                 Username = f.SenderId == userId ? f.Receiver.UserName : f.Sender.UserName,
                 Interests = f.SenderId == userId ?
                     f.Receiver.UsersInterests.Select(ui => ui.Interest.Name).ToList()
                     :
                     f.Sender.UsersInterests.Select(ui => ui.Interest.Name).ToList()
             })
            .ToListAsync();

        public async Task RejectFriendRequestAsync(string senderId, string receiverId)
        {
            var friendship = await FindFriendshipAsync(senderId, receiverId);

            _repository.Remove<Friendship>(friendship);

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
        public Task<List<UserCardModel>> ViewAllPendingRequestAsync(string receiverId) =>
            _repository.AllReadOnly<Friendship>()
            .Where(f => f.ReceiverId == receiverId && f.IsActive == false)
            .Select(f => new UserCardModel()
            {
                UserId = f.Sender.Id,
                Bio = f.Sender.Bio,
                Username = f.Sender.UserName,
                Interests = f.Sender.UsersInterests.Select(ui => ui.Interest.Name).ToList(),
            })
            .ToListAsync();


        private Task<Friendship?> FindFriendshipAsync(string senderId, string receiverId) =>
            _repository.AllReadOnly<Friendship>()
            .FirstOrDefaultAsync(f => f.SenderId == senderId && f.ReceiverId == receiverId);
    }
}
