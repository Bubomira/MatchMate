using MatchMateCore.Dtos.InterestViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces
{
    internal interface IAdminInterestInterface
    {
        public Task AddNewInterest(InterestPostFormModel interestPostModel);
        public Task EditInterest(InterestEditFormModel interestPostModel);
        public Task DeleteInterest(int interestId);
    }
}
