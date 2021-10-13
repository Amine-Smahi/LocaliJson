using System.Collections.Generic;
using System.Linq;
using LocaliJson.Factories;
using LocaliJson.Middlewares;
using LocaliJson.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace LocaliJson
{
    public static class DependencyService
    {
        public static void AddLocaliJson(this IServiceCollection services)
        {
            services.AddSingleton<LocalizationMiddleware>();
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
        }

        public static void UseLocaliJson(this IApplicationBuilder app, List<AppCulture> cultures)
        {
            var supportedCultures = cultures.Select(culture => culture.GetCulture()).ToList();
            var defaultRequestCulture = cultures.FirstOrDefault(culture => culture.IsDefault)?.GetRequestCulture();
            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = defaultRequestCulture,
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };
            app.UseRequestLocalization(options);
            app.UseMiddleware<LocalizationMiddleware>();
        }
    }
}