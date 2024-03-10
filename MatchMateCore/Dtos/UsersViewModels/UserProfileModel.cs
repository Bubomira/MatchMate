
using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Dtos.UsersViewModels
{
    public class UserProfileModel
    {
        public string UserId { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Bio { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public IList<InterestModel> Interests { get; set; } = new List<InterestModel>();
    }
}
