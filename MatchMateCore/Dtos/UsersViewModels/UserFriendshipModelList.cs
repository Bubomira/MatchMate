

namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserFriendshipModelList
    {
        public const int friendsOnPage = 12;
        public IList<UserCardModel> Friends { get; set; } = new List<UserCardModel>();

        public int TotalFriends { get; set; }

        public string SearchItem { get; set; } = string.Empty;

        public int PageNumber { get; set; } = 1;

        public double TotalPagesCount { get; set; }=0;
    }
}
