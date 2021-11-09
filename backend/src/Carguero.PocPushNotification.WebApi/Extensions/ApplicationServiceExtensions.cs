using System;
using Carguero.PocPushNotification.WebApi.Common;
using Carguero.PocPushNotification.WebApi.Handlers;
using Carguero.PocPushNotification.WebApi.Interfaces;
using Carguero.PocPushNotification.WebApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace Carguero.PocPushNotification.WebApi.Extensions
{
    public static class ApplicationServiceExtensions
    {
        private const string OneSignalUrl = "https://onesignal.com/api";
        
        public static WebApplicationBuilder AddApplicationService(this WebApplicationBuilder appBuilder)
        {
            appBuilder.Services.AddAppServices();
            return appBuilder;
        }

        private static void AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<OneSignalHandler>();
            services.AddServices();
            services.AddRefitApis();
            services.Configure<JsonOptions>(opt =>
                opt.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy());
        }

        private static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, NotificationService>();
        }

        private static void AddRefitApis(this IServiceCollection services)
        {
            services.AddRefitClient<IOneSignalApi>(RefitSettingsHelper.GetSettings())
                .ConfigureHttpClient(client => client.BaseAddress = new Uri(OneSignalUrl))
                .AddHttpMessageHandler<OneSignalHandler>();
        }
    }
}