using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Rewards.Store
{
    public class VoucherModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        private readonly UserService _userService;
        private readonly User_ShopItemService _userShopItemService;
        private IWebHostEnvironment _environment;
        public VoucherModel(ShopItemService shopItemService, UserService userService, User_ShopItemService userShopItemService, IWebHostEnvironment environment)
        {
            _shopItemService = shopItemService;
            _userService = userService;
            _userShopItemService = userShopItemService;
            _environment = environment;
        }
        public List<ShopItem> VoucherList { get; set; } = new();
        public ShopItem ItemBought { get; set; } = new();
        [BindProperty]
        public string ItemID { get; set; }
        public User User { get; set; } = new();
        public void OnGet()
        {
            User = _userService.GetUserFirst();
            VoucherList = _shopItemService.GetShopItemByTypeDesc("Voucher");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            User = _userService.GetUserFirst();
            ItemBought = _shopItemService.GetShopItemById(ItemID);
            if (ItemBought.ItemCount > 0 && User.PointsCurrent >= ItemBought.ItemPrice)
            {
                _shopItemService.BuyShopItem(ItemBought, User);
                _userShopItemService.BuyShopItem(User, ItemBought);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} successfully bought.", ItemBought.ItemName);
                return Redirect("/Rewards/Store/Voucher");
            }
            TempData["FlashMessage.Type"] = "danger";
            TempData["FlashMessage.Text"] = "Item not available.";
            return Redirect("/Rewards/Store/Voucher");

        }
    }
}