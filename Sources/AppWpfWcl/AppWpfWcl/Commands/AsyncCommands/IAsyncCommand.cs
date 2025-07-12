using System.Windows.Input;

namespace AppWpfWcl.Commands.AsyncCommands
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
}
