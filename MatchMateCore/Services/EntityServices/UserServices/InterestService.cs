using MatchMateCore.Dtos.InterestViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices
{
    public class InterestService : IInterestInterface
    {
        private readonly IRepository _repository;
        public InterestService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task AddInterestToUserCollectionAsync(int interestId, string userId)
        {
            await _repository.AddAsync<UserInterest>(new UserInterest()
            {
                InterestId = interestId,
                UserId = userId
            });

            await _repository.SaveChangesAsync();
        }

        public async Task<bool> CheckIfInterestIsAttachedToUser(int interestId, string userId) =>
            await _repository.AllReadOnly<UserInterest>()
            .AnyAsync(ui => ui.UserId == userId && ui.InterestId == interestId);

        public Task<List<InterestModel>> GetAllInterestsForCurrentUserAsync(string userId) =>
           _repository.AllReadOnly<Interest>()
            .Select(i => new InterestModel(i.Id, i.Name)
            {
                IsChecked = i.UserInterest.Any(ui => ui.UserId == userId)
            }).ToListAsync();

        public async Task<bool> CheckIfUserHasAtLeastThreeInterests(string userId) =>
            _repository.All<UserInterest>()
            .Where(ui => ui.UserId == userId)
            .Count() > 3;

        public Task<bool> CheckIfInterestExists(int interestId) =>
             _repository.All<Interest>()
            .AnyAsync(i => i.Id == interestId);

        public async Task RemoveInterestFromUserCollectionAsync(int interestId, string userId)
        {
            var userInterest = await _repository.All<UserInterest>()
                 .FirstOrDefaultAsync(ui => ui.InterestId == interestId && ui.UserId == userId);

            await _repository.Remove<UserInterest>(userInterest);

            await _repository.SaveChangesAsync();
        }
    }
}
