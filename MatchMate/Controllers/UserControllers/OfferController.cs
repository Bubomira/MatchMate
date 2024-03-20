using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    public class OfferController : BaseController
    {
        private readonly IOfferInterface _offerService;
        private readonly IFriendshipInterface _friendshipService;
        public OfferController(IOfferInterface offerInterface,
            IFriendshipInterface friendshipInterface)
        {
            ;
            _offerService = offerInterface;
            _friendshipService = friendshipInterface;
        }
        public async Task<IActionResult> Index([FromQuery] OfferIndexModel offerIndexModel)
        {
            offerIndexModel.Offers = await _offerService.GetOffersAsync(offerIndexModel,User.Id());

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
            model.ReceiverUsername = await _offerService.GetOfferReceiverUsernameAsync(id);
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

            await _offerService.AddOfferAsync(offerPostFormModel, User.Id());

            return RedirectToAction(nameof(Index));
        }
    }
}
