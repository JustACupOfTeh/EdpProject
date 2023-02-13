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

        //makes the TrackingCLASS object
        [BindProperty]
        public TrackingCLASS oneUserTravelInstanceOBJECT { get; set; } = new();


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            //sets the userid to default, need to take ryan's part for this
            oneUserTravelInstanceOBJECT.UserID = "abc";

            oneUserTravelInstanceOBJECT.PointsGained = oneUserTravelInstanceOBJECT.DistanceInstance * 10;

            //adds to TrackingDB
            _TrackingService.AddTrackingInstance(oneUserTravelInstanceOBJECT);

            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = "you have gained " + oneUserTravelInstanceOBJECT.PointsGained + " points";

            return Redirect("/Tracking/TrackingHistory");

            return Page();

        }   //end of onpost









    } //end of class
} //end of namespace
