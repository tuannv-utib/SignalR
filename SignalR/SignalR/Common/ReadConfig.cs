using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SignalR.Common
{
    public static class ReadConfig
    {
        public static string AppSetting(string key, string defaultValue = "")
        {
            using (var scope = ServiceActivator.GetScope())
            {
                var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                return config.GetSection("AppSettings").GetValue<string>(key, defaultValue);
            }
        }
    }
}
