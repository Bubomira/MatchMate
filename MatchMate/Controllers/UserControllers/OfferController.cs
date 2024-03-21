using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces.OfferInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    public class OfferController : BaseController
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
            offerIndexModel.Offers = await _offerSuggesterService.GetOffersAsync(offerIndexModel,User.Id());

            offerIndexModel.NextPageNumber = offerIndexModel.CurrentPageNumber +1;
            offerIndexModel.PrevoiusPageNumber = offerIndexModel.CurrentPageNumber + 1;
            offerIndexModel.TotalPageCount =(int) Math.Ceiling((double)offerIndexModel.AllOffersCount / OfferIndexModel.MaxItemsOnPage);

            return View(offerIndexModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(id, User.Id()))
            {
                RedirectToAction("Index", "Friendship");
            }
            OfferPostFormModel model = new OfferPostFormModel();
            model.ReceiverUsername = await _offerReceiverService.GetOfferReceiverUsernameAsync(id);
            model.ReceiverId = id;

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(OfferPostFormModel offerPostFormModel)
        {

            if (!await _friendshipService.CheckIfThereIsAnActiveFriendshipBetweenUsersAsync(offerPostFormModel.ReceiverId, User.Id()))
            {
                RedirectToAction("Index", "Friendship");
            }

            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            await _offerSuggesterService.AddOfferAsync(offerPostFormModel, User.Id());

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
    }
}
