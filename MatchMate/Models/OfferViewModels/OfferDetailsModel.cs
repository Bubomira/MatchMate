using MatchMateInfrastructure.Enums;

namespace MatchMate.Models.OfferViewModels
{
    public class OfferDetailsModel : OfferPreviewModel
    {
        public string Description { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;
    }
}
