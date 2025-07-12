using AppWpfWcl.Interfaces;
using AppWpfWcl.ViewModels;
using AppWpfWcl.Views;

namespace AppWpfWcl.Services
{
    public class SignUpDialogService : ISignUpDialogService
    {
        public bool? ShowDialog(object viewModel)
        {
            var signUpWindow = new SignUpWindow { DataContext = viewModel };            
            ((SignUpViewModel)viewModel).СonfirmAction = new Action(() => { signUpWindow.DialogResult = true; });
            ((SignUpViewModel)viewModel).CancelAction = new Action(() => { signUpWindow.DialogResult = false; });
            return signUpWindow.ShowDialog();            
        }
    }
}
