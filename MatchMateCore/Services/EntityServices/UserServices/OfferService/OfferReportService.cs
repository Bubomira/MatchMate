using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace MatchMateCore.Services.EntityServices.UserServices.OfferService
{
    public class OfferReportService : IReportOfferInterface
    {
        private readonly IRepository _repository;
        public OfferReportService(IRepository repository)
        {
            _repository = repository;
        }
        public Task<bool> CheckIfTheCurrentUserCanBeReporter(string userId, int offerId) =>
            _repository.AllReadOnly<Offer>()
            .AnyAsync(o => o.Id == offerId && o.ReceivingUserId == userId);


        public async Task ReportOffer(int offerId,OfferReportPostModel offerReportModel)
        {
            ReportedOffer reportedOffer = new ReportedOffer()
            {
                OfferId = offerId,
                Comment = offerReportModel.Comment,
                ReasonForRepport = offerReportModel.ReasonForReport
            };

            await _repository.AddAsync<ReportedOffer>(reportedOffer);

            await _repository.SaveChangesAsync();
        }
    }
}
