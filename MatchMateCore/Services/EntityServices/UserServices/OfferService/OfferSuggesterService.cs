using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Dtos.UsersViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices.OfferService
{
    public class OfferSuggesterService : IOfferSuggesterInterface
    {
        private readonly IRepository _repository;
        public OfferSuggesterService(IRepository repository)
        {
            _repository = repository;
        }

        public Task<OfferDetailsModel> GetOfferDetailsAsync(int offerId) =>
            _repository.AllReadOnly<Offer>()
            .Where(o => o.Id == offerId)
            .Select(o => new OfferDetailsModel(
                o.Id, o.Title, o.Status,
                o.SuggestingUserId, o.SuggestingUser.UserName,
                o.ReceivingUserId, o.ReceivingUser.UserName,
                o.Description, o.Place, o.Time))
            .FirstAsync();

        public async Task AddOfferAsync(OfferPostFormModel offerPostFormModel, string senderId)
        {
            var offer = new Offer()
            {
                Description = offerPostFormModel.Description,
                Place = offerPostFormModel.Place,
                Title = offerPostFormModel.Title,
                Status = OfferStatus.Pending,
                Time = offerPostFormModel.Time,
                SuggestingUserId = senderId,
                ReceivingUserId = offerPostFormModel.ReceiverId
            };

            await _repository.AddAsync(offer);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteOfferAsync(int offerId)
        {
            var offer = await _repository.All<Offer>()
                .FirstOrDefaultAsync(o => o.Id == offerId);

            _repository.Remove<Offer>(offer);

            await _repository.SaveChangesAsync();
        }

        public async Task EditOfferAsync(OfferEditFormModel offerEditFormModel)
        {
            var offer = await _repository.All<Offer>()
               .FirstOrDefaultAsync(o => o.Id == offerEditFormModel.Id);

            offer.Title = offerEditFormModel.Title;
            offer.Description = offerEditFormModel.Description;
            offer.Place = offerEditFormModel.Place;

            await _repository.SaveChangesAsync();
        }

        public async Task<List<OfferPreviewModel>> GetOffersAsync(OfferIndexModel offerIndexModel, string userId)
        {
            var offers = _repository.AllReadOnly<Offer>();

            switch (offerIndexModel.IsOfferReceiver)
            {
                case IsOfferReceiver.Yes:
                    offers = offers.Where(o => o.ReceivingUserId == userId);
                    break;
                case IsOfferReceiver.No:
                    offers = offers.Where(o => o.SuggestingUserId == userId);
                    break;
                case IsOfferReceiver.DoesntMatter:
                    offers = offers.Where(o => o.ReceivingUserId == userId
                    || o.SuggestingUserId == userId);
                    break;
            }


            if (!string.IsNullOrEmpty(offerIndexModel.SearchString))
            {
                string search = offerIndexModel.SearchString.ToLower();
                offers = offers.Where(o => o.Title.ToLower().Contains(search)
                || o.Description.ToLower().Contains(search));
            }

            switch (offerIndexModel.Status)
            {
                case OfferStatus.Pending:
                    offers = offers.Where(o => o.Status == OfferStatus.Pending);
                    break;
                case OfferStatus.Accepted:
                    offers = offers.Where(o => o.Status == OfferStatus.Accepted);
                    break;
                case OfferStatus.Cancelled:
                    offers = offers.Where(o => o.Status == OfferStatus.Cancelled);
                    break;
            }

            switch (offerIndexModel.OfferTimeType)
            {
                case TimeTypeOffer.Before:
                    offers = offers.Where(o => o.Time < DateTime.Now);
                    break;
                case TimeTypeOffer.After:
                    offers = offers.Where(o => o.Time > DateTime.Now);
                    break;
            }

            offerIndexModel.AllOffersCount = offers.Count();

            return await offers
            .OrderBy(o => o.Time)
            .Skip((offerIndexModel.CurrentPageNumber - 1) * OfferIndexModel.MaxItemsOnPage)
            .Take(OfferIndexModel.MaxItemsOnPage)
            .Select(o => new OfferPreviewModel(
                o.Id, o.Title, o.Status,
                o.SuggestingUserId, o.SuggestingUser.UserName,
                o.ReceivingUserId, o.ReceivingUser.UserName))
                .ToListAsync();
        }

        public Task<bool> CheckIfOfferExists(int offerId) =>
            _repository.AllReadOnly<Offer>()
            .AnyAsync(o => o.Id == offerId);

        public Task<bool> CheckIfOfferIsSuggestedByUser(int offerId, string userId) =>
            _repository.AllReadOnly<Offer>()
            .AnyAsync(o => o.Id == offerId && o.SuggestingUserId == userId);


    }
}
