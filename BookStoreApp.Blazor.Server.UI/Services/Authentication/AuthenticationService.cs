using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;
using BookStoreApp.Blazor.Server.UI.Providers;
using Microsoft.AspNetCore.Components.Authorization;



namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IClient httpClient;
		private readonly ILocalStorageService localStorage;
		private readonly AuthenticationStateProvider authenticationStateProvider;

		public AuthenticationService(IClient httpClient, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
		{
			this.httpClient = httpClient;
			this.localStorage = localStorage;
			this.authenticationStateProvider = authenticationStateProvider;
		}

		public async Task<bool> AuthenticateAsync(LoginUserDto loginModel)
		{
			var response = await httpClient.LoginAsync(loginModel);

			// Store Token (just can string & number)
			await localStorage.SetItemAsync("accessToken", response.Token);

			// Change auth state of app (with new Provider)



			return true;
		}
	}
}
