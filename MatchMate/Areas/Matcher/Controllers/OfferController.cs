using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;

using static MatchMateInfrastructure.DataConstants;

namespace MatchMate.Areas.Matcher.Controllers
{
    public class OfferController : BaseUserController
    {
        private readonly IOfferSuggesterInterface _offerSuggesterService;
        private readonly IOfferReceiverInterface _offerReceiverService;
        private readonly IFriendshipInterface _friendshipService;
        public OfferController(IOfferSuggesterInterface offerInterface,
            IFriendshipInterface friendshipInterface,
            IOfferReceiverInterface offerReceiverService)
        {
            ;
            _offerSuggesterService = offerInterface;
            _friendshipService = friendshipInterface;
            _offerReceiverService = offerReceiverService;
        }
        public async Task<IActionResult> Index([FromQuery] OfferIndexModel offerIndexModel)
        {
            offerIndexModel.Offers = await _offerSuggesterService.GetOffersAsync(offerIndexModel, User.Id());

            if (offerIndexModel.Offers.Count() == 0 && offerIndexModel.CurrentPageNumber != 1)
            {
                return RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            offerIndexModel.TotalPageCount = (int)Math.Ceiling((double)offerIndexModel.AllOffersCount / OfferIndexModel.MaxItemsOnPage);

            return View(offerIndexModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(id, User.Id()))
            {
                return RedirectToAction("Index", "Friendship", new { pageNumber = 1 });
            }
            OfferPostFormModel model = new OfferPostFormModel();
            model.ReceiverUsername = await _offerReceiverService.GetOfferReceiverUsernameAsync(id);
            model.ReceiverId = id;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(OfferPostFormModel offerPostFormModel)
        {

            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(offerPostFormModel.ReceiverId, User.Id()))
            {
                RedirectToAction("Index", "Friendship");
            }

            var time = DateTime.Now;

            if (!DateTime.TryParseExact(offerPostFormModel.Time, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
            {
                ModelState.AddModelError("Time", "Invalid date time format!");
            }

            if (!ModelState.IsValid)
            {
                return View(offerPostFormModel);
            }

            await _offerSuggesterService.AddOfferAsync(offerPostFormModel, User.Id(), time);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!await _offerSuggesterService.CheckIfOfferExists(id))
            {
                return RedirectToAction(nameof(Index));
            }

            var offerDetails = await _offerSuggesterService.GetOfferDetailsAsync(id);

            return View(offerDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!await _offerSuggesterService.CheckIfOfferIsSuggestedByUser(id, User.Id()))
            {
                return RedirectToAction(nameof(Index));
            }
            var offerDetailed = await _offerSuggesterService.GetOfferDetailsAsync(id);

            var offerEditModel = await _offerSuggesterService.GetOfferEditableDataAsync(id);

            return View(offerEditModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, OfferEditFormModel offerEditModel)
        {
            var time = DateTime.Now;

            if (!DateTime.TryParseExact(offerEditModel.Time, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out time))
            {
                ModelState.AddModelError("Time", "Invalid date time format!");
            }

            if (!ModelState.IsValid)
            {
                return View(offerEditModel);
            }

            if (!await _offerSuggesterService.CheckIfOfferIsSuggestedByUser(id, User.Id()))
            {
                return RedirectToAction(nameof(Index));
            }

            await _offerSuggesterService.EditOfferAsync(offerEditModel, time);

            return RedirectToAction(nameof(Details), new { id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _offerSuggesterService.CheckIfOfferIsSuggestedByUser(id, User.Id()))
            {
                await _offerSuggesterService.DeleteOfferAsync(id);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
