using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Areas.Admin.Controllers
{
    public class ReportedOffersController : BaseAdminController
    {
        private readonly IAdminReportOfferInterface _adminReportService;
        private readonly IOfferSuggesterInterface _offerService;

        public ReportedOffersController(IAdminReportOfferInterface adminReportService,
            IOfferSuggesterInterface offerSuggester)
        {
            _adminReportService = adminReportService;
            _offerService = offerSuggester;
        }

        public async Task<IActionResult> Index([FromQuery] ReportedOfferListModel reportOfferListModel)
        {
            reportOfferListModel.ReportedOffers = await _adminReportService.GetAllReportedOffers(reportOfferListModel);
            reportOfferListModel.TotalPageCount = Math.Ceiling((double)reportOfferListModel.AllOffersCount / ReportedOfferListModel.MaxItemsOnPage);
            if (reportOfferListModel.AllOffersCount == 0 && reportOfferListModel.CurrentPageNumber != 1)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(reportOfferListModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (!await _offerService.CheckIfOfferExists(id))
            {
                return RedirectToAction(nameof(Index));
            }

            var offer = await _adminReportService.GetReportedOfferDetails(id);

            return View(offer);
        }

        public async Task<IActionResult> Strike(int id)
        {
            if (!await _offerService.CheckIfOfferExists(id))
            {
                return RedirectToAction(nameof(Index));
            }

            await _adminReportService.ValidateReport(id);

            return RedirectToAction(nameof(Details), new { id = id });
        }

        public async Task<IActionResult> Unstrike(int id)
        {
            if (!await _offerService.CheckIfOfferExists(id))
            {
                return RedirectToAction(nameof(Index));
            }

            await _adminReportService.DisvalidateReport(id);

            return RedirectToAction(nameof(Details), new { id = id });
        }
    }
}
