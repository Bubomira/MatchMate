
namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserMatchList
    {
        public int CurrentPageNumber { get; set; }

        public int NextPageNumber { get; set; }
        public int PrevoiusPageNumber { get; set; }

        public IList<UserCardModel> Users { get; set; } = new List<UserCardModel>();
    }
}
