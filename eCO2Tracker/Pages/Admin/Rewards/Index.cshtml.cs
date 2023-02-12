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
        [BindProperty]
        public string ShopItemDelete { get; set; } = string.Empty;
        public int GoPageBack = 1;
        public int GoPageFront = 1;
        public int TotalPages = 1;
        public int CurrentPage = 1;
        public int PageLeak = 0;
        public IActionResult OnGet(int index = 1)
        {

            List<ShopItem>? shopitemlist = _shopItemService.GetAll();

            // Configure page navigation
            int PageSize = 9;
            GoPageBack = index -1;
            GoPageFront = index + 1;
            TotalPages = shopitemlist.Count / PageSize;
            PageLeak = shopitemlist.Count % PageSize;
            CurrentPage = index;

            //Get shop items
            if (shopitemlist != null)
            {
                ShopItemList = shopitemlist.Skip((index - 1) * PageSize).Take(PageSize).ToList(); ;
                return Page();
            }
            else
            {
                return Redirect("/Index");
            }
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _shopItemService.DeleteShopItem(ShopItemDelete);
                return Redirect("/Admin/Rewards");
            }
            return Page();
        }
    }
}
