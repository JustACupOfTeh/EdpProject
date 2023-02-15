using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Lifestyle
{
    public class IndexModel : PageModel
    {
        private readonly LifestyleService _lifestyleService;
        public Lifestyles AllLifestyles { get; set; } = new();

        public IndexModel(LifestyleService lifestyleService)
        { 
            _lifestyleService = lifestyleService;
        }

        public List<Lifestyles> LifestyleList { get; set; } = new();
        public void OnGet()
        {
            LifestyleList = _lifestyleService.GetAll();
        }
    }
}
