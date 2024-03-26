
using MatchMateCore.Dtos.BlockedUserModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.UserInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices.UserService
{
    public class UserBlockService : IUserBlockInterface
    {
        private readonly IRepository _repository;
        public UserBlockService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task BlockUser(string wantedToBlockId, string blockerId)
        {
            var friendship = await _repository.All<Friendship>()
                .Where(f => (f.SenderId == wantedToBlockId && f.ReceiverId == blockerId)
                || f.SenderId == blockerId && f.ReceiverId == wantedToBlockId)
                .FirstOrDefaultAsync();

            if (friendship != null)
            {
                await _repository.Remove<Friendship>(friendship);
            }

            var offers = await _repository.All<Offer>()
                .Where(o => (o.SuggestingUserId == wantedToBlockId && o.ReceivingUserId == blockerId)
                || o.SuggestingUserId == blockerId && o.ReceivingUserId == wantedToBlockId)
                .ToListAsync();

            if (offers.Count != 0)
            {
                await _repository.RemoveAll(offers);
            }

            BlockedUsers blockedUser = new BlockedUsers()
            {
                BlockedUserId = wantedToBlockId,
                BlockerUserId = blockerId
            };

            await _repository.AddAsync<BlockedUsers>(blockedUser);

            await _repository.SaveChangesAsync();
        }

        public Task<List<BlockedUserCard>> ShowAllBlockedUsers(BlockedUserList blockedUserList, string blockerId) =>
            _repository.AllReadOnly<BlockedUsers>()
            .Where(bu => bu.BlockerUserId == blockerId)
            .OrderBy(bu => bu.Id)
            .Skip((blockedUserList.CurrentPage - 1) * BlockedUserList.BlockedUsersOnPage)
            .Select(bu => new BlockedUserCard()
            {
                Id = bu.BlockedUserId,
                Username = bu.BlockedUser.UserName,
                Bio = bu.BlockedUser.Bio
            })
            .ToListAsync();


        public async Task UnblockUser(string wantedToUnblockId, string blockerId)
        {
            var blockedEntity = await _repository.All<BlockedUsers>()
                .Where(bu => bu.BlockerUserId == blockerId && bu.BlockerUserId == wantedToUnblockId)
                .FirstAsync();

            await _repository.Remove<BlockedUsers>(blockedEntity);

            await _repository.SaveChangesAsync();
        }


        public Task<bool> CheckIfUserHasBeenBlocked(string blockerId, string blockedId) =>
            _repository.AllReadOnly<BlockedUsers>()
            .AnyAsync(bu => bu.BlockerUserId == blockerId && bu.BlockedUserId == blockedId);

    }
}
