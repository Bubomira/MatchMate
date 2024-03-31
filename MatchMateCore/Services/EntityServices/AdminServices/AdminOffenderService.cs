using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;
using MatchMateCore.Dtos.UsersViewModels.UserAdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.AdminServices
{
    public class AdminOffenderService : IAdminOffenderInterface
    {
        private readonly IRepository _repository;
        public AdminOffenderService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> CheckIfUserHasMoreThanThreeValidlyReportedOffers(string userId) =>
           _repository.AllReadOnly<ReportedOffer>()
           .Where(ro => ro.Offer.SuggestingUserId == userId)
           .Count(ro => ro.IsReasonable == true) >= 3;

        public Task DisableAccount(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<OffenderModel?> GetOffenderDetails(string userId) =>
            _repository.AllReadOnly<ApplicationUser>()
            .Where(au => au.Id == userId)
            .Select(au => new OffenderModel()
            {
                Id = au.Id,
                ReportedOffers = au.SuggestedOffers
                   .Where(so => so.ReportedOffer != null)
                   .Select(su => new ReportedOfferModel()
                   {
                       Id = su.Id,
                       Title = su.Title,
                       ReasonForReport = su.ReportedOffer.ReasonForRepport
                   })
                .ToList(),
                Username = au.UserName
            })
            .FirstOrDefaultAsync();

    }
}
