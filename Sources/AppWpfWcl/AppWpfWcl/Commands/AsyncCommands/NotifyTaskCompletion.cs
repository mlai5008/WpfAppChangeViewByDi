using System.ComponentModel;

namespace AppWpfWcl.Commands.AsyncCommands
{
    public sealed class NotifyTaskCompletion<TResult> : INotifyPropertyChanged
    {
        public NotifyTaskCompletion(Task<TResult> task)
        {
            Task = task;
            if (!task.IsCompleted)
                TaskCompletion = WatchTaskAsync(task);
        }

        public Task TaskCompletion { get; set; }

        public Task<TResult> Task { get; private set; }

        public bool IsCompleted { get { return Task.IsCompleted; } }

        private async Task WatchTaskAsync(Task task)
        {
            try
            {
                await task;
            }
            catch
            {
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
