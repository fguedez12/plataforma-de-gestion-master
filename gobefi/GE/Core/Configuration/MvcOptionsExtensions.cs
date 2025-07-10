using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Configuration
{
    public static class MvcOptionsExtensions
    {
        public static void ConfigureModelBindingMessages(this IMvcBuilder mvcBuilder,
           string resourceName = null, string resourceLocation = null)
        {
            mvcBuilder.Services.Configure<MvcOptions>(opt =>
            {
                //// By default, the Resx file name is ModelBindingDefaultMessages.resx:
                //resourceName = resourceName ?? "ModelBindingDefaultMessages";

                //// By default, resources live in same assembly that the Startup class does:
                //resourceLocation = resourceLocation
                //      ?? Assembly.GetExecutingAssembly().GetName().Name;

                //var stringLocalizerFactory = mvcBuilder.Services
                //        .BuildServiceProvider().GetService<IStringLocalizerFactory>();
                //var loc = stringLocalizerFactory.Create(resourceName, resourceLocation);
                
                opt.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(
                    prop => $"No se indicó un valor para la propiedad '{prop}'.");
                opt.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(
                    () => $"Se requiere un valor.");
                opt.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(
                    () => $"El cuerpo de la petición es obligatorio.");
                opt.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(
                    prop => $"El valor no es válido.");
                opt.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor(
                    (value, prop) => $"El valor '{value}' no es válido para {prop}.");
                opt.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor(
                    value => $"El valor '{value}' no es válido.");
                opt.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor(
                    prop => $"El valor proporcionado no es válido para '{prop}'.");
                opt.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(
                    () => $"El valor suministrado no es válido.");
                opt.ModelBindingMessageProvider.SetValueIsInvalidAccessor(
                    value => $"El valor '{value}' no es válido.");
                opt.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(
                    prop => $"El campo {prop} debe ser un número.");
                opt.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(
                    () => $"El campo debe ser un número.");
            });
        }
    }
}
