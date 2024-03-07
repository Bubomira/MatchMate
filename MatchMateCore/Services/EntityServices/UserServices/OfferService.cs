using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.UnitOfWork;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class OfferService : IOfferInterface
    {
        private readonly IRepository _repository;
        public OfferService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<List<OfferPreviewModel>> GetAllCancelledOffers(string userId)
        {
            throw new NotImplementedException();
        }
        public Task AddOffer(string senderId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOffer(int offerId)
        {
            throw new NotImplementedException();
        }

        public Task EditOffer(OfferEditFormModel offerEditFormModel)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferPreviewModel>> GetAllAcceptedOffers(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferPreviewModel>> GetAllPendingOffers(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OfferPreviewModel>> GetAllReceivedAndAcceptedOffers(string userId)
        {
            throw new NotImplementedException();
        }

        public Task RejectOffer(int offerId)
        {
            throw new NotImplementedException();
        }
    }
}
