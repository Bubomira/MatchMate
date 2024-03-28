

using MatchMateCore.Dtos.OfferViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces
{
    public interface IAdminReportOfferInterface
    {
        public Task<OfferIndexModel> GetAllReportedOffers();
        public Task<bool> CheckIfUserHasMoreThanThreeValidlyReportedOffers(string userId);
        public Task DisvalidateReport(int offerId);
    }
}
