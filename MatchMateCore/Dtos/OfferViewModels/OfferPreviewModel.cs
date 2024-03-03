using MatchMateInfrastructure.Enums;


namespace MatchMateCore.Dtos.OfferViewModels
{

    public class OfferPreviewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public OfferStatus OfferStatus { get; set; }

        public string SuggestedBy { get; set; } = string.Empty;

        public string ReceivedBy { get; set; } = string.Empty;
    }
}
