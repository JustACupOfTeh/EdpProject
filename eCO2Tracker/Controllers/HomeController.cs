using eCO2TrackerWebApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Text;

namespace eCO2TrackerWebApp.Controllers
	{
	public class HomeController : Controller
		{
		private readonly ILogger<HomeController> _logger;

		System.Uri baseAddress = new Uri("https://localhost:7269/api/user/");
		HttpClient httpClient;

		public HomeController(ILogger<HomeController> logger)
			{
			_logger = logger;
			httpClient = new HttpClient();
			httpClient.BaseAddress = baseAddress;
			}

		public IActionResult Index()
			{
			return View();
			}


		public IActionResult Login()
			{
			ClaimsPrincipal claimUser = HttpContext.User;
			if (claimUser.Identity.IsAuthenticated)
				{
				return RedirectToAction("Index", "Home");
				}

			return View("Login");
			}


		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public IActionResult Login(string email, string password)
			{
			UserView? model = new UserView();

			//Initialize these fields in model by filling sample input
			model.Id = "8af15971-7b5f-4efa-17f7-08dafb8dbe8f";
			model.CreatedDate = DateTime.Now;
			model.Role = "User";
			model.FirstName = "A";
			model.LastName = "B";

			model.Email = email;
			model.Password = password;

			string data = JsonConvert.SerializeObject(model);

			string uri = httpClient.BaseAddress + "login";
			HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");

			HttpResponseMessage response = httpClient.PostAsync(uri, content).Result;
			if (response.IsSuccessStatusCode)
				{
				UserView? loggedinUser = new UserView();
				string user = response.Content.ReadAsStringAsync().Result;
				loggedinUser = JsonConvert.DeserializeObject<UserView>(user);

				HttpContext.Session.SetString("FirstName", loggedinUser.FirstName is null ? "" : loggedinUser.FirstName);
				HttpContext.Session.SetString("LastName", loggedinUser.LastName is null ? "" : loggedinUser.LastName);
				HttpContext.Session.SetString("Id", loggedinUser.Id is null ? "" : loggedinUser.Id);

				ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
				ViewBag.LastName = HttpContext.Session.GetString("LastName");
				ViewBag.Id = HttpContext.Session.GetString("Id");


				List<Claim> claims = new List<Claim>() {
				  new Claim(ClaimTypes.NameIdentifier,email),
				   new Claim("OtherProperties","Example Role")
			  };

				ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

				AuthenticationProperties properties = new AuthenticationProperties()
					{
					AllowRefresh = true,
					IsPersistent = true
					};

				HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);

				return this.RedirectToAction("Index", "User", new { firstName = ViewBag.FirstName, lastName = ViewBag.LastName, id = ViewBag.Id }); ;
				}

			ViewBag.Error = "Login failure. Please try again.";



			return View();
			}


		public IActionResult Create()
			{
			return View("Create");
			//return this.RedirectToAction("Create", "User");
			}

		[HttpPost]
		public IActionResult Create(UserView model)
			{
			//Initialize these fields in model by filling sample input
			model.Id = "8af15971-7b5f-4efa-17f7-08dafb8dbe8f";
			model.CreatedDate = DateTime.Now;
			model.Role = "User";

			string data = JsonConvert.SerializeObject(model);
			HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");

			string uri = httpClient.BaseAddress + "register";
			HttpResponseMessage response = httpClient.PostAsync(uri, content).Result;

			if (response.IsSuccessStatusCode)
				{

				return RedirectToAction("Index");
				}
			return View();

			}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
			{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
			}
		}
	}