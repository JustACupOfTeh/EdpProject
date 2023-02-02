using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;

namespace eCO2Tracker.Pages.Admin.Rewards
{
    public class UpdateModel : PageModel
    {
        private readonly ShopItemService _shopItemService;
        private readonly UserService _userService;
        private IWebHostEnvironment _environment;
        public UpdateModel(ShopItemService shopItemService, UserService userService, IWebHostEnvironment environment)
        {
            _shopItemService = shopItemService;
            _userService = userService;
            _environment = environment;
        }
        public List<ShopItem> ShopItemList { get; set; } = new();
        [BindProperty]
        public ShopItem ShopItem { get; set; } = new();
        [BindProperty]
        public IFormFile? ItemImage { get; set; }
        [BindProperty]
        public string ExpireDateTime { get; set; }
        public IActionResult OnGet(string id)
        {
            ShopItemList = _shopItemService.GetAll();

            ShopItem? shopItem = _shopItemService.GetShopItemById(id);
            if (shopItem != null)
            {
                ShopItem = shopItem;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Shop item ID {0} not found", id);
                return Redirect("/Admin/Rewards/Index");
            }
        }
    
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var uploadsFolder = "uploads";
                if (ShopItem.ImageURL != null)
                {
                    var oldImageFile = Path.GetFileName(ShopItem.ImageURL);
                    var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                    var imageFile = Guid.NewGuid() + Path.GetExtension(ItemImage.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await ItemImage.CopyToAsync(fileStream);
                    ShopItem.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                _shopItemService.UpdateShopItem(ShopItem);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Shop item {0} is updated", ShopItem.ItemName);
            }
            return Page();
        }
    }
}
