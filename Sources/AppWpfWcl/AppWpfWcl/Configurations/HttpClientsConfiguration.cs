using AppWpfWcl.HttpCommunications;
using AppWpfWcl.Interfaces;
using AppWpfWcl.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace AppWpfWcl.Configurations
{
    public static class HttpClientsConfiguration
    {
        #region Methods
        public static IServiceCollection AddHttpClients(this IServiceCollection services)
        {
            services.AddHttpClient<IUserHttpClient, UserHttpClient>((sp, httpClient) =>
            {
                var config = sp.GetRequiredService<IOptions<AppSettings>>().Value;
                var baseAddress = config.BaseAddress;
                httpClient.BaseAddress = new Uri(baseAddress);
            });            
            return services;
        }
        #endregion
    }
}
