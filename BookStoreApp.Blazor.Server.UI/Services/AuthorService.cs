using Blazored.LocalStorage;
using BookStoreApp.Blazor.Server.UI.Services;
using BookStoreApp.Blazor.Server.UI.Services.Base;

namespace BookStoreApp.Blazor.Server.UI.Services
{
    public class AuthorService : BaseHttpServices, IAuthorService
    {
        private readonly IClient client;
        private readonly ILocalStorageService localstorage;

        public AuthorService(IClient client, ILocalStorageService localstorage) : base(client, localstorage)
        {
            this.client = client;
            this.localstorage = localstorage;
        }

        public async Task<Response<List<AuthorReadOnlyDto>>> GetAuthors()
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
    }
}
