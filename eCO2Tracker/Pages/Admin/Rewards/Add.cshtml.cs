using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;
using eCO2Tracker.Services;
using System.Net;
using System.Globalization;

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
        [BindProperty]
        public IFormFile? ItemImage { get; set; }
        [BindProperty]
        public string ExpireDateTime { get; set; }
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
                if (ItemImage != null)
                {
                    if (ItemImage.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("ItemImage", "Image size can only be up to 5mb.");
                        return Page();
                    }
                    if (!ItemImage.ContentType.StartsWith("image/"))
                    {
                        ModelState.AddModelError("ItemImage", "File uploaded was not a image");
                        return Page();
                    }

                    //Upload Image

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(ItemImage.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    Directory.CreateDirectory(Path.GetDirectoryName(imagePath));
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await ItemImage.CopyToAsync(fileStream);
                    ShopItem.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }
                //Expire At value
                var format = "yyyy-MM-ddTHH:mm";
                ShopItem.ExpiresAt = DateTime.ParseExact(ExpireDateTime, format, CultureInfo.InvariantCulture);
                _shopItemService.AddShopItem(ShopItem);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("ShopItem {0} is added", ShopItem.ItemName);
                return Redirect("/Admin/Rewards");
            }
            return Page();
        }
    }
}
