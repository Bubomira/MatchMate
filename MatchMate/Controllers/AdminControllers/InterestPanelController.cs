using MatchMateCore.Dtos.InterestViewModels.AdminViewModels;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.AdminControllers
{
    [Authorize(Roles = "Administrator")]
    public class InterestPanelController : BaseController
    {
        private readonly IAdminInterestInterface _adminInterestService;
        public InterestPanelController(IAdminInterestInterface adminInterestInterface)
        {
            _adminInterestService = adminInterestInterface;
        }
        public async Task<IActionResult> Index(int pageNumber)
        {
            InterestPanelList interestPanelModel = new InterestPanelList(pageNumber);

            interestPanelModel.Interests = await _adminInterestService.GetAllInterestsAsync(interestPanelModel);

            if (interestPanelModel.Interests.Count == 0)
            {
                return RedirectToAction(nameof(Index), new { pageNumber = 1 });
            }

            interestPanelModel.TotalPagesCount = Math.Ceiling((double)interestPanelModel.TotalInterestsCount / InterestPanelList.CountOnPage);

            return View(interestPanelModel);
        }

        public async Task<IActionResult> Add(InterestPostFormModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _adminInterestService.CheckIfThereIsAnInterestByNameAsync(model.Name.ToLower()))
                {
                    await _adminInterestService.AddNewInterestAsync(model);
                }
            }

            return RedirectToAction(nameof(Index), new { pageNumber = 1 });
        }

    }
}
