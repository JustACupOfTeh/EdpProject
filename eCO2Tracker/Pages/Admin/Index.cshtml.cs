using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly User_ShopItemService _userShopItemsService;
        public IndexModel(User_ShopItemService userShopItemsService)
        {
            _userShopItemsService = userShopItemsService;
        }
        public void OnGet()
        {
        }
        public void OnPost() 
        { 
            _userShopItemsService.DeleteAllUserShopItems();
        }
    }
}
