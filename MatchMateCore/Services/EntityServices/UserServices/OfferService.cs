using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Dtos.UsersViewModels;
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

            await _repository.AddAsync<Offer>(offer);

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


        public Task RejectOfferAsync(int offerId)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetOfferReceiverUsernameAsync(string userId) =>
        _repository.AllReadOnly<ApplicationUser>()
        .Where(au => au.Id == userId)
        .Select(au => au.UserName)
        .FirstOrDefaultAsync();

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

            string search = offerIndexModel.SearchString.ToLower();

            if (!string.IsNullOrEmpty(search))
            {
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

            return await offers
            .OrderBy(o=>o.Time)
            .Skip(offerIndexModel.CurrentPageNumber-1*OfferIndexModel.MaxItemsOnPage)
            .Take(OfferIndexModel.MaxItemsOnPage)
            .Select(o => new OfferPreviewModel()
            {
                OfferStatus = o.Status,
                Id = o.Id,
                ReceivedBy = o.ReceivingUser.UserName,
                SuggestedBy = o.SuggestingUser.UserName,
                Title = o.Title
            })
            .ToListAsync();

        }
    }
}
