using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using MatchMateInfrastructure.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Areas.Matcher.Controllers
{
    public class OfferStatusController : BaseUserController
    {
        private readonly IOfferReceiverInterface _offerReceiverInterface;
        private readonly IReportOfferInterface _reportOfferService;
        public OfferStatusController(IOfferReceiverInterface offerReceiverInterface,
           IReportOfferInterface reportOfferService)
        {
            _offerReceiverInterface = offerReceiverInterface;
            _reportOfferService = reportOfferService;
        }

        public async Task<IActionResult> Accept(int id)
        {
            if (!await _offerReceiverInterface.CheckIfOfferStatusIsCorrectByStatusAsync(id, User.Id(), OfferStatus.Pending))
            {
                return RedirectToAction("Index", "Offer");
            }

            await _offerReceiverInterface.AcceptOfferAsync(id);

            return RedirectToAction("Details", "Offer", new { id });
        }

        public async Task<IActionResult> Reject(int id)
        {
            if (await _offerReceiverInterface.CheckIfOfferStatusIsCorrectByStatusAsync(id, User.Id(), OfferStatus.Pending))
            {
                await _offerReceiverInterface.RejectOfferAsync(id);
            }
            return RedirectToAction("Index", "Offer");

        }

        public async Task<IActionResult> Cancel(int id)
        {
            if (!await _offerReceiverInterface.CheckIfOfferStatusIsCorrectByStatusAsync(id, User.Id(), OfferStatus.Accepted))
            {
                return RedirectToAction("Index", "Offer");
            }

            await _offerReceiverInterface.CancelOfferAsync(id);

            return RedirectToAction("Details", "Offer", new { id });
        }

        public async Task<IActionResult> Renew(int id)
        {
            if (!await _offerReceiverInterface.CheckIfOfferStatusIsCorrectByStatusAsync(id, User.Id(), OfferStatus.Cancelled))
            {
                return RedirectToAction("Index", "Offer");
            }

            await _offerReceiverInterface.AcceptOfferAsync(id);

            return RedirectToAction("Details", "Offer", new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Report(int id)
        {
            if (!await _reportOfferService.CheckIfTheCurrentUserCanBeReporter(User.Id(), id))
            {
                return RedirectToAction("Index", "Offer", new { pageNumber = 1 });
            }
            OfferReportPostModel offerReportPostModel = new OfferReportPostModel()
            {
                Id = id
            };

            return View(offerReportPostModel);
        }

        [HttpPost]
        public async Task<IActionResult> Report(int id, OfferReportPostModel offerRepostPostModel)
        {
            if (await _reportOfferService.CheckIfTheCurrentUserCanBeReporter(User.Id(), id))
            {
                await _reportOfferService.ReportOffer(id, offerRepostPostModel);
            }
            return RedirectToAction("Index", "Offer", new { pageNumber = 1 });
        }
    }
}
