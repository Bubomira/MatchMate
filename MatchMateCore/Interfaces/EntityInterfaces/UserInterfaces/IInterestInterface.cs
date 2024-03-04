using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    public interface IInterestInterface
    { 
        public Task AddInterestToUserCollectionAsync(int interestId, string userId);
        public Task RemoveInterestFromUserCollectionAsync(int interestId, string userId);
        public Task<bool> CheckIfInterestIsAttachedToUser(int interestId, string userId);
        public Task<bool> CheckIfUserHasAtLeastThreeInterests(string userId);
        public Task<List<InterestModel>> GetAllInterestsForCurrentUserAsync(string userId);

    }
}
