using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eCO2Tracker;
using eCO2Tracker.Models;
using eCO2Tracker.Services;
using System.Xml.Linq;

namespace eCO2Tracker.Pages.Activity
{
	[IgnoreAntiforgeryToken]
	public class DetailsModel : PageModel
	{
		private readonly IActivityService _activityService;

		public DetailsModel(IActivityService activityService)
		{
			_activityService = activityService;
		}

		public eCO2Tracker.Models.Activity? Activity { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			Activity = await _activityService.GetActivityDetails(id);
			return Page();
		}

		public async Task<IActionResult> OnPostUpdatePerformedStatusAsync([FromBody] Request request)
		{
			await _activityService.UpdatePerformedStatus(request.Id);

			return new JsonResult("Status" + true);
		}
	}
}
