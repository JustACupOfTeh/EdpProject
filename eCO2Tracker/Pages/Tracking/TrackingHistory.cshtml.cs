using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Tracking
{
    public class TrackingHistoryModel : PageModel
    {
        //uses functions from TrackingService.cs
        private readonly TrackingService _trackingService;
        public TrackingHistoryModel(TrackingService trackingService)
        {
            _trackingService = trackingService;
        }

        //initialise the TrackingList
        public List<TrackingCLASS> trackingList { get; set; } = new();

        // initialise the class 
        [BindProperty]
        public TrackingCLASS oneTrackingInstance { get; set; } = new();

        //sets values for page
        public int GoPageBack = 1;
        public int GoPageFront = 1;

        //runs when the page loads
        public IActionResult OnGet(int index = 1)
        {
            int PageSize = 5; //sets the number of rows in the page
            GoPageBack = index - 1;
            GoPageFront = index + 1;

            List<TrackingCLASS>? TrackingList = _trackingService.GetTrackingList();
            if (TrackingList != null)
            {
                //skips the number of "rows" and takes the remaining to a list 
                // eg: 1st page --> skips (1-1)*(3) and takes (3) "rows" to display
                trackingList = TrackingList.Skip((index - 1) * PageSize).Take(PageSize).ToList();
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }


        } //end of onget

        //resets sorting to default
        public IActionResult OnPostBackToDefault(int index = 1)
        {
            int PageSize = 5; //sets the number of rows in the page
            GoPageBack = index - 1;
            GoPageFront = index + 1;

            List<TrackingCLASS>? TrackingList = _trackingService.GetTrackingList();
            if (TrackingList != null)
            {
                //skips the number of "rows" and takes the remaining to a list 
                // eg: 1st page --> skips (1-1)*(3) and takes (3) "rows" to display
                trackingList = TrackingList.Skip((index - 1) * PageSize).Take(PageSize).ToList();
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }

        //sorts by date
        public IActionResult OnPostSortByDate(int index = 1)
        {
            int PageSize = 10; //sets the number of rows in the page
            GoPageBack = index - 1;
            GoPageFront = index + 1;

            List<TrackingCLASS>? TrackingList = _trackingService.GetTrackingListByDate();
            if (TrackingList != null)
            {
                //skips the number of "rows" and takes the remaining to a list 
                // eg: 1st page --> skips (1-1)*(3) and takes (3) "rows" to display
                trackingList = TrackingList.Skip((index - 1) * PageSize).Take(PageSize).ToList();
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }

        //sorts by Distance Travelled
        public IActionResult OnPostSortByDistanceTravelled(int index = 1)
        {
            int PageSize = 10; //sets the number of rows in the page
            GoPageBack = index - 1;
            GoPageFront = index + 1;

            List<TrackingCLASS>? TrackingList = _trackingService.GetTrackingByDistanceTravelled();
            if (TrackingList != null)
            {
                //skips the number of "rows" and takes the remaining to a list 
                // eg: 1st page --> skips (1-1)*(3) and takes (3) "rows" to display
                trackingList = TrackingList.Skip((index - 1) * PageSize).Take(PageSize).ToList();
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }


        //sorts by Points Gained
        public IActionResult OnPostSortByPointsGained(int index = 1)
        {
            int PageSize = 10; //sets the number of rows in the page
            GoPageBack = index - 1;
            GoPageFront = index + 1;

            List<TrackingCLASS>? TrackingList = _trackingService.GetTrackingByPointsGained();
            if (TrackingList != null)
            {
                //skips the number of "rows" and takes the remaining to a list 
                // eg: 1st page --> skips (1-1)*(3) and takes (3) "rows" to display
                trackingList = TrackingList.Skip((index - 1) * PageSize).Take(PageSize).ToList();
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }


        public IActionResult OnPost()
        {
            try
            {
                _trackingService.DeleteTrackingInstance(oneTrackingInstance); // call the function

                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Tracking instance Deleted");
                return Redirect("/Tracking/TrackingHistory");
            }

            catch (InvalidCastException e)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("ay something wrong");
                return Redirect("/Tracking/TrackingHistory");
            }

            return Page();
        } //end of onpost 

    }
}
