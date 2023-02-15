using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace eCO2Tracker.Pages.Test
{
    public class AddModel : PageModel
    {
        private readonly TestService _testService;

        public AddModel(TestService testService)
        {
            _testService = testService;
        }

        [BindProperty]
        public TestQuestions question { get; set; } = new();
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                TestQuestions? checkQuestion = _testService.GetQuestionById(question.QuestionID);
                if (checkQuestion != null)
                {
                    ModelState.AddModelError("question.QuestionID", "Question alreay exists.");
                    return Page();
                }
                question.QuestionID = _testService.generateUuid();
                _testService.AddQuestion(question);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} has been added", question.QuestionID);
                return Redirect("/Lifestyle");
            }
            return Page();
        }
    }
}
