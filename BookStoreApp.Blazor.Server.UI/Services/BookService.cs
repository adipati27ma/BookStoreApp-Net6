using AutoMapper;
using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
	public class BookService : BaseHttpServices, IBookService
	{
		private readonly IClient client;
		private readonly IMapper mapper;

		//private readonly ILocalStorageService localstorage;

		public BookService(IClient client, ILocalStorageService localstorage, IMapper mapper) : base(client, localstorage)
		{
			this.client = client;
			this.mapper = mapper;
			//this.localstorage = localstorage;
		}

		public async Task<Response<int>> CreateBook(BookCreateDto author)
		{
			Response<int> response = new();
			try
			{
				// attach header, post Data, ...
				await GetBearerToken();
				await client.BooksPOSTAsync(author);
			}
			catch (ApiException exception)
			{

				response = ConvertApiExceptions<int>(exception);
			}

			return response;
		}

		public async Task<Response<int>> EditBook(int id, BookUpdateDto author)
		{
			Response<int> response = new();
			try
			{
				await GetBearerToken();
				await client.BooksPUTAsync(id, author);
			}
			catch (ApiException exception)
			{

				response = ConvertApiExceptions<int>(exception);
			}

			return response;
		}

		public async Task<Response<BookDetailsDto>> Get(int id)
		{
			Response<BookDetailsDto> response;

			try
			{
				// attach header, fetchData, return data & the success flag
				await GetBearerToken();
				var data = await client.BooksGETAsync(id);
				response = new Response<BookDetailsDto>
				{
					Data = data,
					Success = true,
				};
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<BookDetailsDto>(exception);
			}

			return response;
		}

		public async Task<Response<BookUpdateDto>> GetBookForUpdate(int id)
		{
			Response<BookUpdateDto> response;

			try
			{
				// attach header, fetchData, map data type + return data & the success flag
				await GetBearerToken();
				var data = await client.BooksGETAsync(id);
				response = new Response<BookUpdateDto>
				{
					Data = mapper.Map<BookUpdateDto>(data),
					Success = true,
				};
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<BookUpdateDto>(exception);
			}

			return response;
		}

		public async Task<Response<List<BookReadOnlyDto>>> Get()
		{
			Response<List<BookReadOnlyDto>> response;

			try
			{
				// attach header, fetchData, return data & the success flag
				await GetBearerToken();
				var data = await client.BooksAllAsync();
				response = new Response<List<BookReadOnlyDto>>
				{
					Data = data.ToList(),
					Success = true,
				};
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<List<BookReadOnlyDto>>(exception);
			}

			return response;
		}

		public async Task<Response<int>> Delete(int id)
		{
			Response<int> response = new();

			try
			{
				await GetBearerToken();
				await client.BooksDELETEAsync(id);
			}
			catch (ApiException exception)
			{
				response = ConvertApiExceptions<int>(exception);
			}

			return response;
		}
	}
}
