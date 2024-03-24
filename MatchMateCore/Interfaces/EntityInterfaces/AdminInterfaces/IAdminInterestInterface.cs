using MatchMateCore.Dtos.InterestViewModels.AdminViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces
{
    public interface IAdminInterestInterface
    {
        public Task<List<InterestGetModel>> GetAllInterestsAsync(InterestPanelList interestPanelModel);
        public Task AddNewInterestAsync(InterestPostFormModel interestPostModel);
        public Task EditInterestAsync(InterestEditFormModel interestPostModel);
        public Task DeleteInterestAsync(int interestId);
        public Task<bool> CheckIfThereIsAnInterestByNameAsync(string name);
        public Task<bool> CheckIfThereAreAtLeastThreeInterestsAsync();
    }
}
