using MatchMate.Controllers.BaseControllers;
using MatchMateCore.Dtos.OfferViewModels.OfferAdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.AdminControllers
{
    public class ReportedOffersController : BaseAdminController
    {
        private readonly IAdminReportOfferInterface _adminReportService;

        public ReportedOffersController(IAdminReportOfferInterface adminReportService)
        {
            _adminReportService = adminReportService;
        }

        public async Task<IActionResult> Index([FromQuery] ReportedOfferListModel reportOfferListModel)
        {
            reportOfferListModel.ReportedOffers = await _adminReportService.GetAllReportedOffers(reportOfferListModel);
            reportOfferListModel.TotalPageCount = Math.Ceiling((double)reportOfferListModel.AllOffersCount/ ReportedOfferListModel.MaxItemsOnPage);

            return View(reportOfferListModel);
        }
    }
}
