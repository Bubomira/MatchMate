using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
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
        public Task AcceptOfferAsync(int offerId)
        {
            throw new NotImplementedException();
        }

        public Task CancelOfferAsync(int offerId)
        {
            throw new NotImplementedException();
        }

        public Task<string?> GetOfferReceiverUsernameAsync(string userId) =>
        _repository.AllReadOnly<ApplicationUser>()
        .Where(au => au.Id == userId)
        .Select(au => au.UserName)
        .FirstOrDefaultAsync();

        public Task RejectOfferAsync(int offerId)
        {
            throw new NotImplementedException();
        }
    }
}
