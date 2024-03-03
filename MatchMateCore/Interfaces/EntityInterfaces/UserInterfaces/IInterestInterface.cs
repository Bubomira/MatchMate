using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    internal interface IInterestInterface
    {
        public Task<List<InterestModel>> GetAllInterestsAsync();
        public Task AddInterestToUserCollectionAsync(int interestId, string userId);
        public Task RemoveInterestFromUserCollectionAsync(int interestId,string userId);
        public Task<bool> CheckIfInterestIsAttachedToUser(int interestId,string userId);

    }
}
