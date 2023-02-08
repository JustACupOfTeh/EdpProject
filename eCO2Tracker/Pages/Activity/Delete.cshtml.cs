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

namespace eCO2Tracker.Pages.Activity
{
	public class DeleteModel : PageModel
	{
		private readonly IActivityService _activityService;

		public DeleteModel(IActivityService activityService)
		{
			_activityService = activityService;
		}

		[BindProperty]
		public eCO2Tracker.Models.Activity Activity { get; set; }

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			Activity = await _activityService.GetActivityDetails(id);
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			await _activityService.DeleteActivity(id);

			return RedirectToPage("./Index");
		}
	}
}
