using MatchMate.Controllers.BaseControllers;
using MatchMateCore.Interfaces.EntityInterfaces.AdminInterfaces;
using MatchMateCore.Interfaces.MongoInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace MatchMate.Controllers.AdminControllers
{
    public class OffenderController : BaseAdminController
    {
        private readonly IAdminOffenderInterface _offenderService;
        private readonly IProfilePictureInterface _profilePictureService;
        public OffenderController(IAdminOffenderInterface offenderInterface,
            IProfilePictureInterface profilePictureInterface)
        {
            _offenderService = offenderInterface;
            _profilePictureService = profilePictureInterface;
        }
        
        public async Task<IActionResult> Details(string id)
        {
            var offender = await _offenderService.GetOffenderDetails(id);
            if (offender == null || offender.ReportedOffers.Count==0)
            {
                return RedirectToAction("Index","ReportedOffers");
            }

            offender.ImageUrl = await _profilePictureService.GetProfilePictureFromMongoAsync(offender.Id);

            return View(offender);
        }
    }
}
