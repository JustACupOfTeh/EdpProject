using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Tracking
{
    public class TrackingDetailsModel : PageModel
    {
        //uses functions from TrackingService.cs
        private readonly TrackingService _trackingService;
        public TrackingDetailsModel(TrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        //makes the TrackingCLASS object
        [BindProperty]
        public TrackingCLASS oneUserTravelInstanceOBJECT { get; set; } = new();



        //runs when the page loads
        public IActionResult OnGet(int id)
        {
            TrackingCLASS? tracking = _trackingService.GetTrackingById(id);
            if (tracking != null)
            {
                oneUserTravelInstanceOBJECT = tracking;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Tracking ID {0} not found", id);
                return Redirect("/Tracking/TrackingHistory");
            }
        }
    }
}
