using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Services;
using eCO2Tracker.Models;

namespace eCO2Tracker.Pages.Rewards
{
    public class IndexModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        public IndexModel(ShopItemService shopItemService)
        {
            _shopItemService = shopItemService;
        }
        public List<ShopItem> ShopItemList { get; set; } = new();

        public void OnGet()
        {
            ShopItemList = _shopItemService.GetAll();
        }
    }
}
