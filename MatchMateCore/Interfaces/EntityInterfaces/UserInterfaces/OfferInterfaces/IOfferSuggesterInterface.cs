using MatchMateCore.Dtos.OfferViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces
{
    public interface IOfferSuggesterInterface
    {
        public Task<List<OfferPreviewModel>> GetOffersAsync(OfferIndexModel offerIndexModel, string userId);
        public Task AddOfferAsync(OfferPostFormModel offerPostFormModel, string senderId);
        public Task EditOfferAsync(OfferEditFormModel offerEditFormModel);
        public Task DeleteOfferAsync(int offerId);
        public Task<bool> CheckIfOfferExists(int offerId);
        public Task<bool> CheckIfOfferIsSuggestedByUser(int offerId,string userId);
    }
}
