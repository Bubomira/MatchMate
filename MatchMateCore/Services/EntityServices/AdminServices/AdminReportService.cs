﻿using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateInfrastructure.Models;
using MatchMateInfrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using MatchMateInfrastructure.Enums;

using static MatchMateInfrastructure.DataConstants;
using System.Globalization;

namespace MatchMateCore.Services.EntityServices.AdminServices
{
    public class AdminReportService : IAdminReportOfferInterface
    {
        private readonly IRepository _repository;

        public AdminReportService(IRepository repository)
        {
            _repository = repository;
        }
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

            switch (reportedOffersModel.IsReportReasonable)
            {
                case IsReportReasonable.Yes:
                    query = query.Where(ro => ro.IsReasonable);
                    break;
                case IsReportReasonable.No:
                    query = query.Where(ro => ro.IsReasonable==false);
                    break;
            }

            if (!String.IsNullOrEmpty(reportedOffersModel.SearchString))
            {
                var searchString = reportedOffersModel.SearchString.ToLower();
                query = query.Where(ro => ro.Offer.Title.ToLower().Contains(searchString));
            }

            reportedOffersModel.AllOffersCount = query.Count();

            return query
                .OrderBy(ro => ro.Id)
                .Skip((reportedOffersModel.CurrentPageNumber - 1) * ReportedOfferListModel.MaxItemsOnPage)
                .Take(ReportedOfferListModel.MaxItemsOnPage)
                .Select(ro => new ReportedOfferModel
                {
                    Id = ro.OfferId,
                    ReasonForReport = ro.ReasonForRepport,
                    Title = ro.Offer.Title
                })
                .ToListAsync();
        }

        public Task<ReportedOfferDetailsModel> GetReportedOfferDetails(int offerId) =>
            _repository.AllReadOnly<ReportedOffer>()
            .Where(ro => ro.OfferId == offerId)
            .Select(ro => new ReportedOfferDetailsModel
            {
                IsSuggesterOffender= ro.Offer.SuggestingUser.SuggestedOffers.Count(so=>so.ReportedOffer.IsReasonable)>=1,
                ReportNumber = ro.Id,
                Title = ro.Offer.Title,
                Comment = ro.Comment,
                ReasonForReport = ro.ReasonForRepport,
                Id = ro.OfferId,
                SuggesterId = ro.Offer.SuggestingUserId,
                Description = ro.Offer.Description,
                Place = ro.Offer.Place,
                Time = ro.Offer.Time.ToString(DateTimeFormat,CultureInfo.InvariantCulture),
                IsValidated= ro.IsReasonable
            })
            .FirstAsync();
       
    }
}
