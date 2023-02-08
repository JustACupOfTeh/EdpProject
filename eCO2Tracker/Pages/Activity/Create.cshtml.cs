using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eCO2Tracker;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Activity
{
    public class CreateModel : PageModel
	{
		private readonly IActivityService _activityService;

		public CreateModel(IActivityService activityService)
		{
			_activityService = activityService;
		}

		public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public eCO2Tracker.Models.Activity Activity { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            await _activityService.AddActivity(Activity);

            return RedirectToPage("./Index");
        }
    }
}
