
namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserCardModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string ImageUrl = string.Empty;

        public IList<string> Interests { get; set; } = new List<string>();


    }
}
