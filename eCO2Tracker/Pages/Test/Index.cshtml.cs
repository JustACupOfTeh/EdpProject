using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Test
{
    public class IndexModel : PageModel
    {
        private readonly TestService _testService;

        public IndexModel(TestService testService)
        {
            _testService = testService;
        }
        [BindProperty]
        public TestAnswers answers { get; set; } = new();

        public List<TestQuestions> AllQuestions { get; set; } = new();
        public void OnGet()
        {
            AllQuestions = _testService.GetAll();
        }
        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return Redirect("/Lifestyle");
        //    }
        //}
    }
}
