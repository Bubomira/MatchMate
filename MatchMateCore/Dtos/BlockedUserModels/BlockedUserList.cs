

namespace MatchMateCore.Dtos.BlockedUserModels
{
    public class BlockedUserList
    {
        public const int BlockedUsersOnPage = 10;
        public IList<BlockedUserCard> BlockedUsers { get; set; } = new List<BlockedUserCard>();
        public int TotalBlockedUsersCount { get; set; }
        public int MaxItemsOnPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
