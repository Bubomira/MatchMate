using MatchMateCore.Dtos.OfferViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces
{
    public interface IOfferSuggesterInterface
    {
        public Task<List<OfferPreviewModel>> GetOffersAsync(OfferIndexModel offerIndexModel, string userId);
        public Task<OfferDetailsModel> GetOfferDetailsAsync(int offerId);
        public Task<OfferEditFormModel> GetOfferEditableDataAsync(int offerId);
        public Task AddOfferAsync(OfferPostFormModel offerPostFormModel, string senderId,DateTime time);
        public Task EditOfferAsync(OfferEditFormModel offerEditFormModel,DateTime time);
        public Task DeleteOfferAsync(int offerId);
        public Task<bool> CheckIfOfferExists(int offerId);
        public Task<bool> CheckIfOfferIsSuggestedByUser(int offerId,string userId);
    }
}
