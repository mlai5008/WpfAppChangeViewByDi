using AppWpfWcl.ViewModels.Base;

namespace AppWpfWcl.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        #region Fields
        private string _logIn = null;
        private string _password = null;        
        #endregion

        #region Properties
        public string LogIn
        {
            get => _logIn;
            set
            {
                SetValue(ref _logIn, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                SetValue(ref _password, value);
                OnPropertyChanged(nameof(IsEnabled));
            }
        }

        public bool IsEnabled => !string.IsNullOrWhiteSpace(_logIn) && !string.IsNullOrWhiteSpace(_password);
        #endregion
    }
}
