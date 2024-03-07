using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class OfferService : IOfferInterface
    {
        private readonly IRepository _repository;
        public OfferService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task AddOffer(OfferPostFormModel offerPostFormModel, string senderId)
        {
            var offer = new Offer()
            {
                Description = offerPostFormModel.Description,
                Place = offerPostFormModel.Place,
                Title = offerPostFormModel.Title,
                Status = OfferStatus.Pending,
                SuggestingUserId = senderId,
                ReceivingUserId = offerPostFormModel.ReceivingUserId
            };

            await _repository.AddAsync<Offer>(offer);

            await _repository.SaveChangesAsync();

        }

        public async Task DeleteOffer(int offerId)
        {
            var offer = await _repository.All<Offer>()
                .FirstOrDefaultAsync(o => o.Id == offerId);

            _repository.Remove<Offer>(offer);

            await _repository.SaveChangesAsync();
        }

        public async Task EditOffer(OfferEditFormModel offerEditFormModel)
        {
            var offer = await _repository.All<Offer>()
               .FirstOrDefaultAsync(o => o.Id == offerEditFormModel.Id);

            offer.Title = offerEditFormModel.Title;
            offer.Description = offerEditFormModel.Description;
            offer.Place = offerEditFormModel.Place;

            await _repository.SaveChangesAsync();
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
