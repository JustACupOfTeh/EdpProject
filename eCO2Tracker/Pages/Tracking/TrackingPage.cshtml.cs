using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Tracking
{
    public class TrackingPageModel : PageModel
    {
        private readonly TrackingService _TrackingService;

        public TrackingPageModel(TrackingService UserTravelInstanceService)
        {
            _TrackingService = UserTravelInstanceService;
        }

        [BindProperty]
        public TrackingCLASS oneUserTravelInstanceOBJECT { get; set; } = new();


        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {

                //supposed to add to UserDB
                _TrackingService.AddTrackingInstance(oneUserTravelInstanceOBJECT);
                TempData["FlashMessage.Type"] = "progress tracked!!";
                return Redirect("/Tracking/TrackingResults");

            }

            return Page();

        }
    }
}
