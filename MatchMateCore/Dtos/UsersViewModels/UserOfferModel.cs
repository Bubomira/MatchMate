namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserOfferModel
    {
        public UserOfferModel(string userId, string username)
        {
            UserId = userId;
            Username = username;
        }
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;
    }
}
