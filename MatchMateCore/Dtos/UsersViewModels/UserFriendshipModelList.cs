

namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserFriendshipModelList
    {
        public IList<UserCardModel> Friends { get; set; } = new List<UserCardModel>();

        public int TotalFriends { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int PrevousPage { get; set; }

        public int NextPage { get; set; }

        public double TotalPagesCount { get; set; }=0;
    }
}
