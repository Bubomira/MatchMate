using MatchMateCore.Dtos.UsersViewModels;
using MatchMateInfrastructure.Enums;


namespace MatchMateCore.Dtos.OfferViewModels
{

    public class OfferPreviewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public OfferStatus OfferStatus { get; set; }

        public UserOfferModel SuggestedBy { get; set; } = null!;

        public UserOfferModel ReceivedBy { get; set; } = null!;
    }
}
