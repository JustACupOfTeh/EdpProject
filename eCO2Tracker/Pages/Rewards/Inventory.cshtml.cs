using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Rewards
{
    public class InventoryModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        private readonly UserService _userService;
        private readonly User_ShopItemService _userShopItemService;
        private IWebHostEnvironment _environment;
        public InventoryModel(ShopItemService shopItemService, UserService userService, User_ShopItemService userShopItemService, IWebHostEnvironment environment)
        {
            _shopItemService = shopItemService;
            _userService = userService;
            _userShopItemService= userShopItemService;
            _environment = environment;
        }

        public User User { get; set; } = new();
        public List<User_ShopItems> UserShopItems { get; set; }
        public IActionResult OnGet()
        {
            User? user = _userService.GetUserFirst();
            if (user != null)
            {
                User = user;
                UserShopItems = _userShopItemService.GetShopItems(user.UserID);
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }
    }
}
