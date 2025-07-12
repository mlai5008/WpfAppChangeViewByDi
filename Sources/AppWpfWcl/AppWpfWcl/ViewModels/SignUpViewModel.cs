using AppWpfWcl.Commands;
using AppWpfWcl.ViewModels.Base;

namespace AppWpfWcl.ViewModels
{
    public class SignUpViewModel : ViewModelBase
    {
        #region Fields
        private string _logIn = null;
        private string _password = null;
        private string _firstName = null;
        private string _lastName = null;
        private string _email = null;
        private string _phone = null;
        #endregion

        #region Ctors
        public SignUpViewModel()
        {
            СonfirmCommand = new RelayCommand(OnСonfirmCommandExecuted);
            CancelCommand = new RelayCommand(OnCancelCommandExecuted);

            Initialize();
        } 
        #endregion

        #region Properties
        public string LogIn
        {
            get => _logIn;
            set => SetValue(ref _logIn, value);
        }

        public string Password
        {
            get => _password;
            set => SetValue(ref _password, value);
        }

        public string FirstName
        {
            get => _firstName;
            set => SetValue(ref _firstName, value);
        }

        public string LastName
        {
            get => _lastName;
            set => SetValue(ref _lastName, value);
        }

        public string Email
        {
            get => _email;
            set => SetValue(ref _email, value);
        }

        public string Phone
        {
            get => _phone;
            set => SetValue(ref _phone, value);
        }

        public Action СonfirmAction { get; set; } = null;
        public Action CancelAction { get; set; } = null;
        #endregion

        #region Commands
        public RelayCommand СonfirmCommand { get; }
        public RelayCommand CancelCommand { get; }
        #endregion

        #region Methods
        private void Initialize()
        {
            LogIn = "Test_12";
            Password = "12345";
            FirstName = "HR";
            LastName = "HRL";
            Email = "hr@clerkgroup.by";
            Phone = "+375 (29) 542-57-90";
        }

        private void OnСonfirmCommandExecuted(object obj)
        {
            СonfirmAction();
        }

        private void OnCancelCommandExecuted(object obj)
        {
            CancelAction();
            ClearAll();
        }

        private void ClearAll()
        {
            LogIn = string.Empty;
            Password = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        } 
        #endregion
    }
}
