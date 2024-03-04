
using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Data;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class FriendshipService : IFriendshipInterface
    {
        private readonly ApplicationDbContext _context;
        public FriendshipService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task AcceptFriendRequestAsync(string senderId, string receiverId)
        {
            var friendship = await FindFriendshipAsync(senderId, receiverId);

            friendship.Status = FriendshipStatus.Active;

            await _context.SaveChangesAsync();
        }

        public Task<List<UserCardModel>> GetAllFriendsAsync(string userId) =>
             _context.Friendships
             .Where(f => f.Status == FriendshipStatus.Active &&
             (f.SenderId == userId || f.ReceiverId == userId))
             .Select(f => new UserCardModel()
             {
                 UserId = f.SenderId == userId ? f.ReceiverId : f.SenderId,
                 Birthday = f.SenderId == userId ? f.Receiver.Birthday : f.Sender.Birthday,
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

            friendship.Status = FriendshipStatus.Rejected;

            await _context.SaveChangesAsync();
        }

        public async Task SendFriendRequestAsync(string senderId, string receiverId)
        {
            var friendship = await _context.Friendships
                .FirstOrDefaultAsync(f => f.SenderId == senderId && f.ReceiverId == receiverId);

            if (friendship == null)
            {
                Friendship newFriendship = new Friendship()
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Status = FriendshipStatus.Pending,
                };

                await _context.Friendships.AddAsync(newFriendship);
            }
            else
            {
                friendship.Status = FriendshipStatus.Pending;
            }

            await _context.SaveChangesAsync();

        }

        public Task<List<UserCardModel>> ViewAllPendingRequestAsync(string receiverId) =>
            _context.Friendships.Where(f => f.ReceiverId == receiverId && f.Status == FriendshipStatus.Pending)
            .Select(f => new UserCardModel()
            {
                UserId = f.Sender.Id,
                Bio = f.Sender.Bio,
                Username = f.Sender.UserName,
                Birthday = f.Sender.Birthday,
                Interests = f.Sender.UsersInterests.Select(ui => ui.Interest.Name).ToList(),
            })
            .ToListAsync();


        private Task<Friendship?> FindFriendshipAsync(string senderId, string receiverId) =>
            _context.Friendships
            .FirstOrDefaultAsync(f => f.SenderId == senderId && f.ReceiverId == receiverId);
    }
}
