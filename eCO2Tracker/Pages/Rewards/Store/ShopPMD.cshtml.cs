using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Rewards.Store
{
    public class ShopPMDModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        private readonly UserService _userService;
        private IWebHostEnvironment _environment;
        public ShopPMDModel(ShopItemService shopItemService, UserService userService, IWebHostEnvironment environment)
        {
            _shopItemService = shopItemService;
            _userService = userService;
            _environment = environment;
        }
        public List<ShopItem> ShopPMDList { get; set; } = new();
        public ShopItem ItemBought { get; set; } = new();
        [BindProperty]
        public string ItemID { get; set; }
        public User User { get; set; } = new();
        public void OnGet()
        {
            User = _userService.GetUserFirst();
            ShopPMDList = _shopItemService.GetShopItemByType("PMD");
        }

        public async Task<IActionResult> OnPostAsync() 
        {
            User = _userService.GetUserFirst();
            ItemBought = _shopItemService.GetShopItemById(ItemID);
            if (ItemBought.ItemCount > 0 && User.PointsCurrent > 0) 
            {
                _shopItemService.BuyShopItem(ItemBought, User);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} successfully bought.", ItemBought.ItemName);
                return Page();
            }
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = "Item not available.";
            return Redirect("/Rewards/Store/ShopPMD");
            
        }
    }
}
