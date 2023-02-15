using eCO2Tracker.Models;
using eCO2Tracker.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace eCO2Tracker.Pages.Lifestyle
{
    public class AddModel : PageModel
    {
        private readonly LifestyleService _lifestyleService;
        private IWebHostEnvironment _environment;

        public AddModel(LifestyleService lifestyleService, IWebHostEnvironment environment)
        {
            _lifestyleService = lifestyleService;
            _environment = environment;
        }

        [BindProperty]
        public Lifestyles MyLifestyle { get; set; } = new();

        [BindProperty]
        public IFormFile? Upload { get; set; }


        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Lifestyles? lifestyle = _lifestyleService.GetLifestyleById(MyLifestyle.EntryID);
                if (lifestyle != null)
                {
                    ModelState.AddModelError("MyLifestyle.EntryID", "Lifestyle alreay exists.");
                    return Page();
                }

                if (Upload != null)
                {
                    if (Upload.Length > 5 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 5MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyLifestyle.EntryImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }
                MyLifestyle.EntryID = _lifestyleService.generateUuid();
                _lifestyleService.AddLifestyle(MyLifestyle);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} has been added", MyLifestyle.EntryTitle);
                return Redirect("/Lifestyle");
            }
            return Page();
        }
    }
}