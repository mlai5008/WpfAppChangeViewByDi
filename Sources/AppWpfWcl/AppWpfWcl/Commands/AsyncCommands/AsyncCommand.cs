using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace AppWpfWcl.Commands.AsyncCommands
{
    public class AsyncCommand<TResult> : AsyncCommandBase, INotifyPropertyChanged
    {
        private readonly Func<Task<TResult>> _command;

        private NotifyTaskCompletion<TResult> _execution = null;

        public AsyncCommand(Func<Task<TResult>> command)
        {
            _command = command;
        }
        public override bool CanExecute(object parameter)
        {
            return Execution == null || Execution.IsCompleted;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Execution = new NotifyTaskCompletion<TResult>(_command());
            await Execution.TaskCompletion;
            RaiseCanExecuteChanged();
        }

        public NotifyTaskCompletion<TResult> Execution
        {
            get
            {
                return _execution;
            }
            set
            {
                if (value != _execution)
                {
                    _execution = value;
                    OnPropertyChanged();
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged = null;
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
