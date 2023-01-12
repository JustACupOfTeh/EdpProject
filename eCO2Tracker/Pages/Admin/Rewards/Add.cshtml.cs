using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Admin.Rewards
{
    public class AddModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        private readonly UserService _userService;
        private IWebHostEnvironment _environment;
        public AddModel(ShopItemService shopItemService, UserService userService, IWebHostEnvironment environment)
        {
            _shopItemService = shopItemService;
            _userService = userService;
            _environment = environment;
        }
        [BindProperty]
        public ShopItem ShopItem { get; set; } = new();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                ShopItem.ItemID = Guid.NewGuid().ToString();
                ShopItem? employee = _shopItemService.GetShopItemById(ShopItem.ItemID);
                if (employee != null)
                {
                    ModelState.AddModelError("ShopItem.ItemID", "Shop Item ID alreay exists.");
                    return Page();
                }
                _shopItemService.AddShopItem(ShopItem);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("ShopItem {0} is added", ShopItem.ItemName);
                return Redirect("/Admin/Rewards");
            }
            return Page();
        }
    }
}
