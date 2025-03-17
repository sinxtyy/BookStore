using BookStore.Services;
using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BookStore.Security.Jwt;
using System.Transactions;
using Transaction = BookStore.Core.Models.Transaction;

namespace BookStore.Controllers
{
	public class ProfileController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly UserService _userService;
		private readonly TransactionService _transactionService;
		private readonly IJwtProvider _jwtProvider;
		private readonly string? _userLogin;

		public ProfileController(ILogger<HomeController> logger, TransactionService transactionService, IJwtProvider jwtProvider, UserService userService, IHttpContextAccessor httpContextAccessor)
		{
			_logger = logger;
			_userService = userService;
			_jwtProvider = jwtProvider;
			_transactionService = transactionService;

			_userLogin = _jwtProvider.GetLoginFromCookie();
		}

		public async Task<IActionResult> Home()
		{
			if (_userLogin != null)
				return View(await _userService.SearchUserAsLogin(_userLogin));

			string[] res = { "Name", "example@gmail.com" };

			return View(res);
		}

		public async Task<IActionResult> Orders()
		{
			List<Transaction>? res = await _transactionService.SearchTransactionsAsAddresser(_userLogin);

			return View(res);
		}

		public IActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
		public async Task<IActionResult> Login(string login, string password)
		{
			// if(!Validator.IsValidPassword(password) || !Validator.IsValidLogin(login))
			// {
			// 	ModelState.AddModelError("ValidationException", "Некоректний логін або пароль");
            // 	return View();
			// }

			if(await _userService.Login(login, password))
			{
				return RedirectToAction(nameof(Home));			
			}

			ModelState.AddModelError("LoginException", "Не вдалося ввійти в аккаунт, спробуйте пізніше або зверніться в службу підтримки");
            return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(string login, string email, string password)
		{
			// if(!Validator.IsValidPassword(password) || !Validator.IsValidLogin(login) || !Validator.IsValidEmail(email))
			// {
			// 	ModelState.AddModelError("ValidationException", "Некоректні пошта, логін або пароль");
            // 	return View();
			// }

			if(await _userService.Register(login, password, email))
			{
				return RedirectToAction(nameof(Home));
			}

			ModelState.AddModelError("RegisterException", "Не вдалося зареєструватися, спробуйте пізніше або зверніться в службу підтримки");
            return View();
		}
	}
}
