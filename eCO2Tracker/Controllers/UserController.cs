using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
//using Microsoft.AspNetCore.Http;
using eCO2TrackerWebApp.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace eCO2TrackerWebApp.Controllers
	{
	[Authorize]
	public class UserController : Controller
		{
		private readonly ILogger<UserController> _logger;

		System.Uri baseAddress = new Uri("https://localhost:7269/api/user/");
		HttpClient httpClient;

		public UserController(ILogger<UserController> logger)
			{
			_logger = logger;
			httpClient = new HttpClient();
			httpClient.BaseAddress = baseAddress;
			}

		public async Task<IActionResult> Index(string firstName, string lastName, string id)
			{
			ViewBag.FirstName = firstName;
			ViewBag.LastName = lastName;
			ViewBag.Id = id;

			HttpContext.Session.SetString("FirstName", firstName is null ? "" : firstName);
			HttpContext.Session.SetString("LastName", lastName is null ? "" : lastName);
			HttpContext.Session.SetString("Id", id is null ? "" : id);

			List<UserView>? modelList = new List<UserView>();

			string uri = httpClient.BaseAddress + "list";
			HttpResponseMessage response = await httpClient.GetAsync(uri);

			if (response.IsSuccessStatusCode)
				{
				string data = response.Content.ReadAsStringAsync().Result;
				modelList = JsonConvert.DeserializeObject<List<UserView>>(data);
				}

			return View(modelList);
			}

		public IActionResult Create()
			{

			return View();
			}



		public IActionResult Update(string id)
			{
			ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
			ViewBag.LastName = HttpContext.Session.GetString("LastName");
			ViewBag.Id = HttpContext.Session.GetString("Id");

			UserView? model = new UserView();

			string uri = httpClient.BaseAddress + "view?id=" + id;
			HttpResponseMessage response = httpClient.GetAsync(uri).Result;
			if (response.IsSuccessStatusCode)
				{
				string data = response.Content.ReadAsStringAsync().Result;
				model = JsonConvert.DeserializeObject<UserView>(data);

				}

			return View("Update", model);
			}


		[HttpPost]
		public IActionResult Update(UserView model)
			{
			ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
			ViewBag.LastName = HttpContext.Session.GetString("LastName");
			ViewBag.Id = HttpContext.Session.GetString("Id");

			//Initialize these fields (not updated) in model by filling sample input
			model.Email = "abc@gmail.com";
			model.CreatedDate = DateTime.Now;
			model.Role = "User";

			string data = JsonConvert.SerializeObject(model);
			StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

			string uri = httpClient.BaseAddress + "update";
			HttpResponseMessage response = httpClient.PutAsync(uri, content).Result;

			if (response.IsSuccessStatusCode)
				{
				return RedirectToAction("Index");
				}

			return View("Update");
			}

		public IActionResult Delete(string id)
			{
			ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
			ViewBag.LastName = HttpContext.Session.GetString("LastName");
			ViewBag.Id = HttpContext.Session.GetString("Id");

			string uri = httpClient.BaseAddress + "delete?id=" + id;
			HttpResponseMessage response = httpClient.DeleteAsync(uri).Result;
			if (response.IsSuccessStatusCode)
				{

				return RedirectToAction("Index");
				}

			return View("");
			}

		public async Task<IActionResult> Logout()
			{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			HttpContext.Session.Clear();
			return this.RedirectToAction("Index", "Home");
			}

		public IActionResult ShowReferralCode(string id)
			{
			string uri = httpClient.BaseAddress + "getReferralCode?id=" + id;
			HttpResponseMessage response = httpClient.GetAsync(uri).Result;
			if (response.IsSuccessStatusCode)
				{
				string referralCode = "";
				referralCode = response.Content.ReadAsStringAsync().Result;

				ViewBag.ReferralCode = referralCode;
				ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
				ViewBag.LastName = HttpContext.Session.GetString("LastName");
				ViewBag.Id = id;

				return View("ReferralCode");
				//HttpContext.Session.SetString("ReferralCode", firstName is null ? "" : firstName);
				//return RedirectToAction("ReferralCode" , "User", new { referralCode = referralCode });
				}

			return View("");

			}
		}
	}
