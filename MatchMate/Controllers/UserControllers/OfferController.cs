using MatchMateCore.Dtos.OfferViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.UserInterfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatchMate.Controllers.UserControllers
{
    public class OfferController : BaseController
    {
        private readonly IOfferInterface _offerInterface;
        public OfferController(IOfferInterface offerInterface)
        {;
            _offerInterface = offerInterface;
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create(string id)
        {
            OfferPostFormModel model = new OfferPostFormModel();
            model.ReceiverUsername = await _offerInterface.GetOfferReceiverUsername(id);
            model.ReceiverId = id;

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(OfferPostFormModel offerPostFormModel)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(Create));
            }

            await _offerInterface.AddOffer(offerPostFormModel, User.Id());

            return RedirectToAction(nameof(Index));
        }
    }
}
