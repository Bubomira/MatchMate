using MatchMateCore.Dtos.OfferViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IOfferInterface
    {
        public Task<List<OfferPreviewModel>> GetAllPendingOffers(string userId);
        public Task<List<OfferPreviewModel>> GetAllAcceptedOffers(string userId);
        public Task<List<OfferPreviewModel>> GetAllReceivedAndAcceptedOffers(string userId);
        public Task AddOffer(OfferPostFormModel offerPostFormModel ,string senderId);
        public Task EditOffer(OfferEditFormModel offerEditFormModel);
        public Task DeleteOffer(int offerId);
        public Task RejectOffer(int  offerId);
    }
}
