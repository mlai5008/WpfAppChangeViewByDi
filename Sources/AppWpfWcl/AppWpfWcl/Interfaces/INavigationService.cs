using AppWpfWcl.ViewModels.Base;

namespace AppWpfWcl.Interfaces
{
    public interface INavigationService
    {
        #region Methods
        ViewModelBase NavigationTo<T>() where T : ViewModelBase; 
        #endregion
    }
}
