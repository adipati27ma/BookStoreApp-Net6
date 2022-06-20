using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BookStoreApp.Blazor.Server.UI.Providers
{
	public class ApiAuthenticationStateProvider : AuthenticationStateProvider
	{
		private readonly ILocalStorageService localStorage;
		private readonly JwtSecurityTokenHandler jwtSecurityTokenHandler;

		public ApiAuthenticationStateProvider(ILocalStorageService localStorage)
		{
			this.localStorage = localStorage;
			jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			// get token in local storage
			var savedToken = await localStorage.GetItemAsync<string>("accessToken");

			// if the token is null, return new claims with the empty user & out the func.
			if (savedToken == null)
			{
				return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
			}

			var tokenContent = jwtSecurityTokenHandler.ReadJwtToken(savedToken);
			var claims = tokenContent.Claims;

			var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));

			return new AuthenticationState(user);
		}
	}
}
