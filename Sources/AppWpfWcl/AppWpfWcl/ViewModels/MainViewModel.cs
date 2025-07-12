using AppWpfWcl.Commands;
using AppWpfWcl.Commands.AsyncCommands;
using AppWpfWcl.Interfaces;
using AppWpfWcl.ViewModels.Base;
using System.Windows;

namespace AppWpfWcl.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        #region Fields
        private ViewModelBase _loginView = null;
        private ViewModelBase _authorizationView = null;
        private readonly INavigationService _navigationService;
        private readonly ISignUpDialogService _signUpDialogService;
        private readonly IUserService _userService;
        private bool _isLogIn = false;
        private bool _isLoading = true;
        #endregion

        #region Ctors
        public MainViewModel(INavigationService navigationService, ISignUpDialogService signUpDialogService, IUserService userService)
        {
            _navigationService = navigationService;
            _signUpDialogService = signUpDialogService;
            _userService = userService;
            NavigationToAuthorizationViewCommand = new RelayCommand(OnNavigationToAuthorizationViewCommandExecuted, canExecute => AuthorizationView != null && AuthorizationView is DeveloperViewModel);
            NavigationToDeveloperViewCommand = new RelayCommand(OnNavigationToDeveloperViewCommandExecuted, canExecute => AuthorizationView != null && (AuthorizationView is UserViewModel || AuthorizationView is AuthorizationViewModel));
            AsyncCommand = new AsyncCommand<string>(DelayMethod);
            LogInCommand = new AsyncCommand<string>(OnLogInCommandExecuted);
            LogOutCommand = new AsyncCommand<string>(OnLogOutCommandExecuted);
            SignUpCommand = new AsyncCommand<string>(OnSignUpCommandExecuted);
            Initialize();
        }
        #endregion

        #region Properties
        public ViewModelBase AuthorizationView
        {
            get => _authorizationView;
            set => SetValue(ref _authorizationView, value);
        }

        public ViewModelBase LoginView
        {
            get => _loginView;
            set => SetValue(ref _loginView, value);
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetValue(ref _isLoading, value);
                OnPropertyChanged(nameof(LoadingVisibility));
            }
        }

        public Visibility LoadingVisibility => IsLoading ? Visibility.Collapsed : Visibility.Visible;
        #endregion

        #region Commands
        public RelayCommand NavigationToAuthorizationViewCommand { get; }
        public RelayCommand NavigationToDeveloperViewCommand { get; }
        public IAsyncCommand AsyncCommand { get; }
        public IAsyncCommand LogInCommand { get; }
        public IAsyncCommand LogOutCommand { get; }
        public IAsyncCommand SignUpCommand { get; }        
        #endregion

        #region Methods
        private void Initialize()
        {
            AuthorizationView = _navigationService.NavigationTo<AuthorizationViewModel>();
            LoginView = _navigationService.NavigationTo<LoginViewModel>();
        }

        private void OnNavigationToAuthorizationViewCommandExecuted(object obj)
        {            
            AuthorizationView = _isLogIn ? _navigationService.NavigationTo<UserViewModel>() : _navigationService.NavigationTo<AuthorizationViewModel>();
        }

        private void OnNavigationToDeveloperViewCommandExecuted(object obj)
        {
            AuthorizationView = _navigationService.NavigationTo<DeveloperViewModel>();
        }

        private async Task<string> DelayMethod()
        {
            IsLoading = false;
            await Task.Delay(5000);
            IsLoading = true;
            return "OK";
        }

        private async Task<string> OnLogInCommandExecuted()
        {
            var logIn = ((LoginViewModel)LoginView).LogIn;
            var password = ((LoginViewModel)LoginView).Password;
            IsLoading = false;
            var baseResponse = await _userService.LogInAsync(logIn, password);
            IsLoading = true;
            string result = baseResponse != null ? baseResponse.Code.ToString() : "500";
            if (result == "200")
            {
                IsLoading = false;
                var user = await _userService.GetUserAsync(logIn);
                IsLoading = true;
                if (user.Username != null && user.Password != null)
                {
                    _isLogIn = true;
                    OnNavigationToAuthorizationViewCommandExecuted(null);                    
                    var userViewModel = (UserViewModel)_navigationService.NavigationTo<UserViewModel>();
                    userViewModel.Id = user.Id;
                    userViewModel.FirstName = user.FirstName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.Email = user.Email;
                    userViewModel.Phone = user.Phone;
                    LoginView = _navigationService.NavigationTo<LogOutViewModel>();
                }                
            }
            return "OK";
        }

        private async Task<string> OnLogOutCommandExecuted()
        {
            IsLoading = false;
            var baseResponse = await _userService.LogOutAsync();
            IsLoading = true;
            string result = baseResponse != null ? baseResponse.Code.ToString() : "500";
            if (result == "200")
            {
                _isLogIn = false;                
                OnNavigationToAuthorizationViewCommandExecuted(null);
                LoginView = _navigationService.NavigationTo<LoginViewModel>();
            }
            return "OK";
        }

        private async Task<string> OnSignUpCommandExecuted()
        {
            string result = string.Empty;
            var viewModel = (SignUpViewModel)_navigationService.NavigationTo<SignUpViewModel>();
            var dialogResult = _signUpDialogService.ShowDialog(viewModel);
            if (dialogResult.HasValue && dialogResult.Value)
            {
                IsLoading = false;
                var baseResponse = await _userService.CreateUserAsync(viewModel.LogIn, viewModel.FirstName, viewModel.LastName, viewModel.Email, viewModel.Password, viewModel.Phone);
                IsLoading = true;
                result = baseResponse != null ? baseResponse.Code.ToString() : "500";
                if (result == "200")
                {
                    _isLogIn = true;
                    IsLoading = false;
                    var user = await _userService.GetUserAsync(viewModel.LogIn);
                    IsLoading = true;
                    var userViewModel = (UserViewModel)_navigationService.NavigationTo<UserViewModel>();
                    userViewModel.Id = user.Id;
                    userViewModel.FirstName = user.FirstName;
                    userViewModel.LastName = user.LastName;
                    userViewModel.Email = user.Email;
                    userViewModel.Phone = user.Phone;
                    NavigationToAuthorizationViewCommand.Execute(null);
                    OnNavigationToAuthorizationViewCommandExecuted(null);
                    LoginView = _navigationService.NavigationTo<LogOutViewModel>();
                }
            }
            await Task.Delay(5);
            return result;
        }        
        #endregion
    }
}
