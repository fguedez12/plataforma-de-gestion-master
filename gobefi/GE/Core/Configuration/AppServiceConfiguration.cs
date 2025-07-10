using GobEfi.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace GobEfi.Web.Core.Configuration
{
    public class AppServiceConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
        }
    }
}
