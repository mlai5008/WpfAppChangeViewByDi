using AppWpfWcl.Configurations;
using AppWpfWcl.Interfaces;
using AppWpfWcl.Services;
using AppWpfWcl.Settings;
using AppWpfWcl.ViewModels;
using AppWpfWcl.ViewModels.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace AppWpfWcl
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            serviceCollection.AddSingleton<MainViewModel>();
            serviceCollection.AddSingleton<AuthorizationViewModel>();
            serviceCollection.AddSingleton<UserViewModel>();
            serviceCollection.AddSingleton<DeveloperViewModel>();
            serviceCollection.AddSingleton<LoginViewModel>();
            serviceCollection.AddSingleton<SignUpViewModel>();
            serviceCollection.AddSingleton<LogOutViewModel>();
            serviceCollection.AddSingleton<INavigationService, NavigationService>();
            serviceCollection.AddSingleton<IUserService, UserService>();
            serviceCollection.AddSingleton<ISignUpDialogService, SignUpDialogService>();
            serviceCollection.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider => viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();            

            serviceCollection.Configure<AppSettings>(configuration.GetSection("AppSettings"));

            serviceCollection.AddHttpClients();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }
    }
}
