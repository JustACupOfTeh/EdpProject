using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eCO2Tracker;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Activity
{
	public class EditModel : PageModel
	{
		private readonly IActivityService _activityService;

		public EditModel(IActivityService activityService)
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

		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see https://aka.ms/RazorPagesCRUD.
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}

			await _activityService.EditActivity(Activity);

			return RedirectToPage("./Index");
		}

	}
}
