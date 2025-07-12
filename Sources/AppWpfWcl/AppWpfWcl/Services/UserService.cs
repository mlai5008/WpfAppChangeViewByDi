using AppWpfWcl.Interfaces;
using AppWpfWcl.Models;

namespace AppWpfWcl.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUserHttpClient _userHttpClient;
        #endregion

        #region Ctors
        public UserService(IUserHttpClient userHttpClient)
        {
            _userHttpClient = userHttpClient;
        }
        #endregion

        #region Methods
        public async Task<BaseResponse> CreateUserAsync(string username, string firstName, string lastName, string email, string password, string phone)
        {
            return await _userHttpClient.CreateUserAsync(username, firstName, lastName, email, password, phone);
        }

        public async Task<BaseResponse> LogInAsync(string logIn, string password)
        {
            return await _userHttpClient.LogInAsync(logIn, password);
        }

        public async Task<BaseResponse> LogOutAsync()
        {
            return await _userHttpClient.LogOutAsync();
        }

        public async Task<User> GetUserAsync(string userName)
        {
            return await _userHttpClient.GetUserAsync(userName);
        }        
        #endregion
    }
}
