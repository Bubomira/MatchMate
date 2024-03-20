using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Dtos.UsersViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IOfferInterface
    {
        public Task<string?> GetOfferReceiverUsernameAsync(string userId);
        public Task<List<OfferPreviewModel>> GetOffersAsync(OfferIndexModel offerIndexModel, string userId);
        public Task AddOfferAsync(OfferPostFormModel offerPostFormModel ,string senderId);
        public Task EditOfferAsync(OfferEditFormModel offerEditFormModel);
        public Task DeleteOfferAsync(int offerId);
        public Task RejectOfferAsync(int  offerId);
    }
}
