

using MatchMateCore.Dtos.OfferViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces
{
    public interface IReportOfferInterface
    {
        public Task ReportOffer(int offerId, OfferReportPostModel offerReportModel);
        public Task<bool> CheckIfTheCurrentUserCanBeReporter(string userId, int offerId);
    }
}
