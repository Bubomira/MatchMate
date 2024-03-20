

using MatchMateInfrastructure.Enums;

namespace MatchMateCore.Dtos.OfferViewModels
{
    public class OfferIndexModel
    {
        public const int MaxItemsOnPage = 6;
        public string SearchString { get; set; } = string.Empty;

        public int AllOffersCount { get; set; }

        public OfferStatus? Status { get; set; } = OfferStatus.Accepted;

        public IsOfferReceiver IsOfferReceiver { get; set; } = IsOfferReceiver.Yes;

        public TimeTypeOffer OfferTimeType { get; set; } = TimeTypeOffer.After;

        public int TotalPageCount { get; set; }

        public int CurrentPageNumber { get; set; } = 1;

        public int PrevoiusPageNumber { get; set; }

        public int NextPageNumber { get; set; }

        public IList<OfferPreviewModel> Offers { get; set; } = new List<OfferPreviewModel>();
    }
}
