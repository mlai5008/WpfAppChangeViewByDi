using AppWpfWcl.ViewModels.Base;

namespace AppWpfWcl.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        #region Fields        
        private long _id;
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _phone;
        #endregion
        
        #region Properties        
        public long Id
        {
            get => _id;
            set => SetValue(ref _id, value);
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
        #endregion
    }
}
