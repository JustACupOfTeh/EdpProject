using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using eCO2Tracker.Models;
using eCO2Tracker.Services;

namespace eCO2Tracker.Pages.Lifestyle
{
    public class UpdateModel : PageModel
    {
        private readonly LifestyleService _lifestyleService;
        private IWebHostEnvironment _environment;

        public UpdateModel(LifestyleService lifestyleService, IWebHostEnvironment environment)
        {
            _lifestyleService = lifestyleService;
            _environment = environment;
        }

        [BindProperty]
        public Lifestyles MyLifestyle { get; set; } = new();

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public IActionResult OnGet(string id)
        {
            Lifestyles? lifestyle = _lifestyleService.GetLifestyleById(id);
            if (lifestyle != null)
            {
                MyLifestyle = lifestyle;
                MyLifestyle.EntryID = lifestyle.EntryID;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Lifestyle ID {0} not found", id);
                return Redirect("/Lifestyle");
            }
        }

        public async Task<IActionResult> OnPostUpdateAsync()
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    if (MyLifestyle.EntryImageURL != null)
                    {
                        var oldImageFile = Path.GetFileName(MyLifestyle.EntryImageURL);
                        var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyLifestyle.EntryImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }
                _lifestyleService.UpdateLifestyle(MyLifestyle);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Lifestyle ID {0} has been updated", MyLifestyle.EntryID);
                return Redirect("/Lifestyle");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRemoveAsync()
        {
            if (ModelState.IsValid)
            {
                _lifestyleService.RemoveLifestyle(MyLifestyle);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Lifestyle ID {0} has been deleted", MyLifestyle.EntryID);
                return Redirect("/Lifestyle");
            }
            return Page();
        }
    }
}