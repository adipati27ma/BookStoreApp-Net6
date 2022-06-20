using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services.Authentication
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly IClient httpClient;
		private readonly ILocalStorageService localStorage;

		public AuthenticationService(IClient httpClient, ILocalStorageService localStorage)
		{
			this.httpClient = httpClient;
			this.localStorage = localStorage;
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
