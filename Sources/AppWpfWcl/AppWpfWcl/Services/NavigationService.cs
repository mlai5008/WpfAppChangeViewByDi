using AppWpfWcl.Interfaces;
using AppWpfWcl.ViewModels.Base;

namespace AppWpfWcl.Services
{
    public class NavigationService : INavigationService
    {
        #region Fields
        private readonly Func<Type, ViewModelBase> _viewModelFactory; 
        #endregion

        #region Ctors
        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        } 
        #endregion

        #region Methods
        public ViewModelBase NavigationTo<TViewModel>() where TViewModel : ViewModelBase
        {
            return _viewModelFactory.Invoke(typeof(TViewModel));
        }
        #endregion
    }
}
