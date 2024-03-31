

namespace MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels
{
    public class ReportedOfferDetailsModel:ReportedOfferModel
    {
        public int ReportNumber { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Place { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string SuggesterId { get; set; } = string.Empty;
        public string? Comment { get; set; } = string.Empty;
        public bool IsValidated { get; set; }
    }
}
