using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BookStoreApp.Blazor.Server.UI.Services.Base
{
	public class BaseHttpServices
	{
		private readonly IClient client;
		private readonly ILocalStorageService localstorage;

		public BaseHttpServices(IClient client, ILocalStorageService localstorage)
		{
			this.client = client;
			this.localstorage = localstorage;
		}

		protected Response<Guid> ConvertApiExceptions<Guid>(ApiException apiException)
		{
			if (apiException.StatusCode == 400)
			{
				return new Response<Guid>
				{
					Message = "Validation errors have occured.",
					ValidationErrors = apiException.Response,
					Success = false
				};
			}

			if (apiException.StatusCode == 404)
			{
				return new Response<Guid>
				{
					Message = "The requested item couldn't be found.",
					Success = false
				};
			}

			return new Response<Guid> { Message = "Something went wrong, please try again.", Success = false };
		}

		// Just to provide/input the bearer token into the Headers
		protected async Task GetBearerToken()
		{
			var token = await localstorage.GetItemAsStringAsync("accessToken");
			if (token != null)
			{
				client.HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
			}
		}
	}
}
