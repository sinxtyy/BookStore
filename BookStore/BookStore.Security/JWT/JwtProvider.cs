using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookStore.Core.Models;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Security.Jwt
{
	public class JwtProvider : IJwtProvider
	{
		private string secretKey => "abrakadabra125!abrakadabra12567!";
		private readonly IHttpContextAccessor _httpContextAccessor;

		public JwtProvider(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}

		public string GenerateToken(string login)
		{
			Claim[] claims = new[]
			{
				new Claim(ClaimTypes.Name, login),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
			};

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
				SecurityAlgorithms.HmacSha256
			);

			var token = new JwtSecurityToken(
				claims: claims,
				signingCredentials: signingCredentials,
				expires: DateTime.Now.AddHours(6)
			);

			var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

			return tokenValue;
		}

		public async Task SetAuthentificationTokenCookie(string token)
		{
			await Task.Run(() =>
				_httpContextAccessor.HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions
				{
					HttpOnly = true,
					Secure = true,
					Expires = DateTime.UtcNow.AddHours(1)
				})
			);
		}

		public string GetLoginFromCookie()
		{
			var handler = new JwtSecurityTokenHandler();

			string token = _httpContextAccessor.HttpContext.Request.Cookies["AuthToken"];

			if (handler.CanReadToken(token))
			{
				var jwtToken = handler.ReadJwtToken(token);

				var login = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

				return login;
			}

			return "";
		}

		//public string UserRole()
		//{
		//    var token = this.GetAuthentificationTokenFromCookie();
		//    if (token == null)
		//        return "user";

		//    var handler = new JwtSecurityTokenHandler();
		//    var key = Encoding.UTF8.GetBytes(secretKey);

		//    try
		//    {
		//        var jwtToken = handler.ReadJwtToken(token);
		//        var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

		//        return roleClaim.ToString();
		//    }
		//    catch
		//    {
		//        return "user";
		//    }
		//}
	}
}