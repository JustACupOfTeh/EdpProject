using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;

namespace eCO2Tracker.Pages.Admin.Rewards
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
        public List<ShopItem> ShopItemList { get; set; } = new();
        public IActionResult OnGet()
        {
            List<ShopItem>? shopitemlist = _shopItemService.GetAll();
            if (shopitemlist != null)
            {
                ShopItemList = shopitemlist;
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }
    }
}
