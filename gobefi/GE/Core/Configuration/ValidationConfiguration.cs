using FluentValidation;
using GobEfi.Web.Core.Validation;
using GobEfi.Web.Models.MedidorModels;
using Microsoft.Extensions.DependencyInjection;

namespace GobEfi.Web.Core.Configuration
{
    public class ValidationConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            //services.AddTransient<IValidator<MedidorModel>, TipoArchivoValidation>();
        }
    }
}
