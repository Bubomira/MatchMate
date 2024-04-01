
using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;

namespace MatchMateCore.Dtos.UsersViewModels.UserAdminViewModels
{
    public class OffenderModel
    {
        public string Id { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public IList<ReportedOfferModel> ReportedOffers { get; set; }
            = new List<ReportedOfferModel>();

    }
}
