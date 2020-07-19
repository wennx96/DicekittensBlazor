using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DicekittensBlazor.Data
{
	public class AccountService : DbContext
	{
		public HttpContext HttpContext { get; set; }

		public DbSet<AccountModel> Users { get; set; }

		public AccountService(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
		{
			HttpContext = httpContextAccessor.HttpContext;
		}

		public void SignUp()
		{ 
		
		}

		public void Update()
		{ 
		
		}

		public async Task Login(string email, string password) 
		{
			AccountModel user = await AuthenticateUser(email, password);

			if (user == null) return;

			List<Claim> claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, user.Username),
				new Claim(ClaimTypes.NameIdentifier, user.UserID)
			};

			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

			ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

			AuthenticationProperties authProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
				ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15),
				IsPersistent = true,
				IssuedUtc = DateTimeOffset.UtcNow
			};

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
				claimsPrincipal, authProperties);
		}

		public async Task Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}

		private async Task<AccountModel> AuthenticateUser(string email, string password)
		{
			AccountModel user = await Users.FirstOrDefaultAsync(user => user.Email == email && user.Password == password);

			return user;
		}
	}
}
