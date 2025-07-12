using AppWpfWcl.Interfaces;
using AppWpfWcl.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace AppWpfWcl.HttpCommunications
{
    public class UserHttpClient : IUserHttpClient
    {
        #region Fields
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options = new() { WriteIndented = true };
        #endregion

        #region Ctors
        public UserHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        #endregion

        #region Methods
        public async Task<BaseResponse> CreateUserAsync(string username, string firstName, string lastName, string email, string password, string phone)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var content = JsonContent.Create(new { id = 0, username, firstName, lastName, email, password, phone, userStatus = 0 });
                //var content = new StringContent("{\"id\":0,\"username\":\"Test_12\",\"firstName\":\"HR\",\"lastName\":\"HRL\",\"email\":\"hr@clerkgroup.by\",\"password\":\"12345\",\"phone\":\"+375 (29) 542-57-90\",\"userStatus\":0}", Encoding.UTF8, "application/json");

                using HttpResponseMessage httpResponse = await _httpClient.PostAsync("/v2/user", content);
                httpResponse.EnsureSuccessStatusCode();
                var statusCode = (int)httpResponse.StatusCode;
                using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                baseResponse = await JsonSerializer.DeserializeAsync<BaseResponse>(responseStream, _options);
            }
            catch (HttpRequestException httpRequestExc)
            {
                baseResponse.Message = httpRequestExc.Message;
            }
            catch (Exception exc)
            {
                baseResponse.Message = exc.Message;
            }
            return baseResponse;
        }

        public async Task<BaseResponse> LogInAsync(string logIn, string password)
        {
            var baseResponse = new BaseResponse();
            try
            {
                var request = $"/v2/user/login?username={logIn}&password={password}";
                using var httpResponse = await _httpClient.GetAsync(request);
                var statusCode = (int)httpResponse.StatusCode;
                using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                baseResponse = await JsonSerializer.DeserializeAsync<BaseResponse>(responseStream, _options);
            }
            catch (HttpRequestException httpRequestExc)
            {
                baseResponse.Message = httpRequestExc.Message;
            }
            catch (Exception exc)
            {
                baseResponse.Message = exc.Message;
            }

            return baseResponse;
        }

        public async Task<BaseResponse> LogOutAsync()
        {
            var baseResponse = new BaseResponse();

            try
            {
                var request = $"/v2/user/logout";
                using var httpResponse = await _httpClient.GetAsync(request);
                var statusCode = (int)httpResponse.StatusCode;
                using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                baseResponse = await JsonSerializer.DeserializeAsync<BaseResponse>(responseStream, _options);
            }
            catch (HttpRequestException httpRequestExc)
            {
                baseResponse.Message = httpRequestExc.Message;
            }
            catch (Exception exc)
            {
                baseResponse.Message = exc.Message;
            }
            return baseResponse;
        }

        public async Task<User> GetUserAsync(string userName)
        {
            var user = new User();
            try
            {                
                var request = $"/v2/user/{userName}";
                using var httpResponse = await _httpClient.GetAsync(request);
                var statusCode = (int)httpResponse.StatusCode;
                using var responseStream = await httpResponse.Content.ReadAsStreamAsync();
                user = await JsonSerializer.DeserializeAsync<User>(responseStream, _options);
            }
            catch (HttpRequestException httpRequestExc)
            {
                user.Message = httpRequestExc.Message;
            }
            catch (Exception exc)
            {
                user.Message = exc.Message;
            }

            return user;
        }
        #endregion
    }
}
