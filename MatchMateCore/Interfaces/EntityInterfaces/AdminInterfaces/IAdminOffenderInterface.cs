
using MatchMateCore.Dtos.UsersViewModels.UserAdminViewModels;

namespace MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces
{
    public interface IAdminOffenderInterface
    {
        public  Task<bool> CheckIfUserHasMoreThanThreeValidlyReportedOffers(string userId);
        public  Task<OffenderModel> GetOffenderDetails(string userId);
        public  Task DisableAccount(string userId);
    }
}
