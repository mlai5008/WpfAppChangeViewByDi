using AppWpfWcl.Models;

namespace AppWpfWcl.Interfaces
{
    public interface IUserService
    {
        #region Methods
        Task<BaseResponse> CreateUserAsync(string username, string firstName, string lastName, string email, string password, string phone);
        Task<BaseResponse> LogInAsync(string logIn, string password);
        Task<BaseResponse> LogOutAsync();
        Task<User> GetUserAsync(string userName);
        #endregion
    }
}
