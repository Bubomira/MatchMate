

using MatchMateInfrastructure.Enums;

namespace MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels
{
    public class ReportedOfferModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public ReasonForReport ReasonForReport { get; set; }
        public string? Comment { get; set; }
    }
}
