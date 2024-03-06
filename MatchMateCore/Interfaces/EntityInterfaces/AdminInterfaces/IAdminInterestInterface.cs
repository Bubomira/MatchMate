using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces
{
    public interface IAdminInterestInterface
    {
        public Task AddNewInterestAsync(InterestPostFormModel interestPostModel);
        public Task EditInterestAsync(InterestEditFormModel interestPostModel);
        public Task DeleteInterestAsync(int interestId);
    }
}
