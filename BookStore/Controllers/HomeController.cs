using BookStore.Services;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BookStore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserService userService;


		public HomeController(ILogger<HomeController> logger, UserService service)
		{
			_logger = logger;
			userService = service;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}

		//public IActionResult RegisterPage()
		//{
		//	return View();
		//}

		//[HttpPost]
		//public IActionResult RegisterPage(string login, string password, string email, string? role = null)
		//{
		//	if(userService.SearchUserAsLogin(login) != null)
		//	{
		//		throw new Exception("\r\nEnter a different login");
		//	}


		//}
	}
}
