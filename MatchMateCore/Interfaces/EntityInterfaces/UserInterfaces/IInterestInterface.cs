using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces
{
    internal interface IInterestInterface
    {
        public Task<List<InterestModel>> GetAllInterestsAsync();
        public Task AddInterestToUserCollection(List<InterestModel> interests, string userId);
        public Task RemoveInterestFromUserCollection(int interestId,string userId);
        public Task<bool> CheckIfInterestExists(int interestId);

    }
}
