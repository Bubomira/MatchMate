using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces
{
    public interface IAdminReportOfferInterface
    {
        public Task<List<ReportedOfferModel>> GetAllReportedOffers(ReportedOfferListModel reportedOffersModel);
        public Task<ReportedOfferDetailsModel> GetReportedOfferDetails(int offerId);
        public Task DisvalidateReport(int offerId);
        public Task ValidateReport(int offerId);
    }
}
