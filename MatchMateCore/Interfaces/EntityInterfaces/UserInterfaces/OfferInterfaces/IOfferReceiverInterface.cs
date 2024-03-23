using MatchMateInfrastructure.Enums;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces
{
    public interface IOfferReceiverInterface
    {
        public Task<string?> GetOfferReceiverUsernameAsync(string userId);
        public Task RejectOfferAsync(int offerId);
        public Task AcceptOfferAsync(int offerId);
        public Task CancelOfferAsync(int offerId);
        public Task<bool> CheckIfOfferStatusIsCorrectByStatusAsync(int offerId,string receiverId,OfferStatus status);
    }
}
