

using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.AdminServices
{
    public class AdminReportService : IAdminReportOfferInterface
    {
        private readonly IRepository _repository;

        public AdminReportService(IRepository repository)
        {
            _repository = repository;
        }
        public async Task<bool> CheckIfUserHasMoreThanThreeValidlyReportedOffers(string userId) =>
           _repository.AllReadOnly<ReportedOffer>()
            .Where(ro => ro.Offer.SuggestingUserId == userId)
            .Count(ro => ro.IsReasonable == true) >= 3;

        public async Task DisvalidateReport(int offerId)
        {
            var reportedOffer = await _repository.All<ReportedOffer>()
                .FirstOrDefaultAsync(ro => ro.OfferId == offerId);

            reportedOffer.IsReasonable = false;

            await _repository.SaveChangesAsync();
        }

        public async Task ValidateReport(int offerId)
        {
            var reportedOffer = await _repository.All<ReportedOffer>()
              .FirstOrDefaultAsync(ro => ro.OfferId == offerId);

            reportedOffer.IsReasonable = true;

            await _repository.SaveChangesAsync();
        }

        public Task<List<ReportedOfferModel>> GetAllReportedOffers(ReportedOfferListModel reportedOffersModel)
        {
            var query = _repository.AllReadOnly<ReportedOffer>();

            reportedOffersModel.AllOffersCount = query.Count();

            return query
                .OrderBy(ro => ro.Id)
                .Skip((reportedOffersModel.CurrentPageNumber - 1) * ReportedOfferListModel.MaxItemsOnPage)
                .Take(ReportedOfferListModel.MaxItemsOnPage)
                .Select(ro => new ReportedOfferModel
                {
                    Id = ro.Id,
                    ReasonForReport = ro.ReasonForRepport,
                    Title = ro.Offer.Title
                })
                .ToListAsync();
        }
    }
}
