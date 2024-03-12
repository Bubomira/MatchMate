
namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserCardModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public bool IsActiveFriendship { get; set; } = false;
        public bool IsPendingFriendship { get; set; } = false;
        public string ImageUrl { get; set; } = string.Empty;

        public IList<string> Interests { get; set; } = new List<string>();


    }
}
