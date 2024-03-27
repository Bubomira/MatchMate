

namespace MatchMateCore.Dtos.BlockedUserModels
{
    public class BlockedUserList
    {
        public BlockedUserList(int currentPage)
        {
            CurrentPage = currentPage;
        }
        public const int BlockedUsersOnPage = 10;
        public IList<BlockedUserCard> BlockedUsers { get; set; } = new List<BlockedUserCard>();
        public int TotalBlockedUsersCount { get; set; }
        public double TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
