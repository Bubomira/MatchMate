using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using MatchMateInfrastructure.Enums;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices.OfferService
{
    public class OfferReceiverService : IOfferReceiverInterface
    {
        private readonly IRepository _repository;
        public OfferReceiverService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task AcceptOfferAsync(int offerId)
        {
            var offer = await _repository.All<Offer>()
                .FirstOrDefaultAsync(o => o.Id == offerId);

            offer.Status = OfferStatus.Accepted;

            await _repository.SaveChangesAsync();
        }

        public async Task CancelOfferAsync(int offerId)
        {
            var offer = await _repository.All<Offer>()
               .FirstOrDefaultAsync(o => o.Id == offerId);

            offer.Status = OfferStatus.Cancelled;

            await _repository.SaveChangesAsync();
        }

        public Task<bool> CheckIfOfferStatusIsCorrectByStatusAsync(int offerId,string receiverId, OfferStatus status) =>
            _repository.AllReadOnly<Offer>()
            .AnyAsync(o => o.Id == offerId && o.ReceivingUserId==receiverId && o.Status == status);
       

        public Task<string?> GetOfferReceiverUsernameAsync(string userId) =>
        _repository.AllReadOnly<ApplicationUser>()
        .Where(au => au.Id == userId)
        .Select(au => au.UserName)
        .FirstOrDefaultAsync();

        public async Task RejectOfferAsync(int offerId)
        {
            var offer = await _repository.All<Offer>()
                .FirstOrDefaultAsync(o => o.Id == offerId);

            await _repository.Remove<Offer>(offer);

            await _repository.SaveChangesAsync();
        }
    }
}
