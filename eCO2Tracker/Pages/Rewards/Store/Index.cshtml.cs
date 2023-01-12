using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Rewards.Store
{
    public class IndexModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        private readonly UserService _userService;
        private IWebHostEnvironment _environment;
        public IndexModel(ShopItemService shopItemService, UserService userService, IWebHostEnvironment environment)
        {
            _shopItemService = shopItemService;
            _userService = userService;
            _environment = environment;
        }

        [BindProperty]
        public User User { get; set; } = new();
        public IActionResult OnGet()
        {
            User? user = _userService.GetUserFirst();
            if (user != null)
            {
                User = user;
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }
    }
}
