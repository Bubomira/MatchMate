
using MatchMateInfrastructure.Enums;

namespace MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels
{
    public class ReportedOfferListModel
    {
        public const int MaxItemsOnPage = 10;
        public string SearchString { get; set; } = string.Empty;
        public int AllOffersCount { get; set; }
        public int TotalPageCount { get; set; }
        public int CurrentPageNumber { get; set; } = 1;
        public IList<ReportedOfferModel> ReportedOffers { get; set; } = new List<ReportedOfferModel>();
    }
}
