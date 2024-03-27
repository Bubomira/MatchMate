
using MatchMateCore.Dtos.BlockedUserModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.UserInterfaces
{
    public interface IUserBlockInterface
    {
        public Task BlockUser(string wantedToBlockId, string blockerId);
        public Task UnblockUser(string wantedToUnblockId, string blockerId);
        public Task<List<BlockedUserCard>> ShowAllBlockedUsers(BlockedUserList blockedUserList,string blockerId);
        public Task<bool> CheckIfUserHasBeenBlocked(string blockerId,string blockedId);
    }
}
