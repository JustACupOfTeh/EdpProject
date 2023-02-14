using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Tracking
{
    public class TrackingPageModel : PageModel
    {
        private readonly TrackingService _TrackingService;
        private readonly UserService _UserService;

        public TrackingPageModel(TrackingService UserTravelInstanceService, UserService userService)
        {
            _TrackingService = UserTravelInstanceService;
            _UserService = userService;
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
            User user = _UserService.GetUserFirst();
            oneUserTravelInstanceOBJECT.UserID = user.UserID;

            //calculates points gained
            oneUserTravelInstanceOBJECT.PointsGained = oneUserTravelInstanceOBJECT.DistanceInstance * 10;

            //calculates carbon emissions saved (in grams)
            //every 10m saves 86g of carbon emissions!!
            oneUserTravelInstanceOBJECT.EnergySaved = oneUserTravelInstanceOBJECT.DistanceInstance / 10 * 86;

            //adds to TrackingDB
            _TrackingService.AddTrackingInstance(oneUserTravelInstanceOBJECT);

            //adds the points to Yishen DB
            _UserService.AddUserPointsBy(oneUserTravelInstanceOBJECT.UserID, oneUserTravelInstanceOBJECT.PointsGained);

            TempData["FlashMessage.Type"] = "success";
            TempData["FlashMessage.Text"] = "you have gained " + oneUserTravelInstanceOBJECT.PointsGained + " points";

            return Redirect("/Tracking/TrackingHistory");

            return Page();

        }   //end of onpost









    } //end of class
} //end of namespace
