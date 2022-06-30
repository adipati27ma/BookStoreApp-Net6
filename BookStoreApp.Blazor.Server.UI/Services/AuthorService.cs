using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
	public class AuthorService : BaseHttpServices, IAuthorService
	{
		private readonly IClient client;
		private readonly IMapper mapper;

		//private readonly ILocalStorageService localstorage;

		public AuthorService(IClient client, ILocalStorageService localstorage, IMapper mapper) : base(client, localstorage)
		{
			this.client = client;
			this.mapper = mapper;
			//this.localstorage = localstorage;
		}

		public async Task<Response<int>> CreateAuthor(AuthorCreateDto author)
		{
			Response<int> response = new();
			try
			{
				// attach header, post Data, ...
				await GetBearerToken();
				await client.AuthorsPOSTAsync(author);
			}
			catch (ApiException exception)
			{

				response = ConvertApiExceptions<int>(exception);
			}

			return response;
		}

		public async Task<Response<int>> EditAuthor(int id, AuthorUpdateDto author)
		{
			Response<int> response = new();
			try
			{
				await GetBearerToken();
				await client.AuthorsPUTAsync(id, author);
			}
			catch (ApiException exception)
			{

				response = ConvertApiExceptions<int>(exception);
			}

			return response;
		}

		public async Task<Response<AuthorDetailsDto>> Get(int id)
		{
			Response<AuthorDetailsDto> response;

			try
			{
				// attach header, fetchData, return data & the success flag
				await GetBearerToken();
				var data = await client.AuthorsGETAsync(id);
				response = new Response<AuthorDetailsDto>
				{
					Data = data,
					Success = true,
				};
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<AuthorDetailsDto>(exception);
			}

			return response;
		}

		public async Task<Response<AuthorUpdateDto>> GetAuthorForUpdate(int id)
		{
			Response<AuthorUpdateDto> response;

			try
			{
				// attach header, fetchData, map data type + return data & the success flag
				await GetBearerToken();
				var data = await client.AuthorsGETAsync(id);
				response = new Response<AuthorUpdateDto>
				{
					Data = mapper.Map<AuthorUpdateDto>(data),
					Success = true,
				};
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<AuthorUpdateDto>(exception);
			}

			return response;
		}

		public async Task<Response<List<AuthorReadOnlyDto>>> Get()
		{
			Response<List<AuthorReadOnlyDto>> response;

			try
			{
				// attach header, fetchData, return data & the success flag
				await GetBearerToken();
				var data = await client.AuthorsAllAsync();
				response = new Response<List<AuthorReadOnlyDto>>
				{
					Data = data.ToList(),
					Success = true,
				};
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<List<AuthorReadOnlyDto>>(exception);
			}

			return response;
		}

		public async Task<Response<int>> Delete(int id)
		{
			Response<int> response = new();

			try
			{
				await GetBearerToken();
				await client.AuthorsDELETEAsync(id);
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<int>(exception);
			}

			return response;
		}
	}
}
