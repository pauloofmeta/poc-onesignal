using Carguero.PocPushNotification.WebApi.Extensions;
using Carguero.PocPushNotification.WebApi.Interfaces;
using Carguero.PocPushNotification.WebApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Carguero.PocPushNotification.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = WebApplication
                .CreateBuilder(args)
                .AddApplicationService()
                .Build();

            app.MapPost("/api/notifications/all", async (HttpRequest req, INotificationService notificationService) =>
            {
                await notificationService.SendAllAsync();
                return Results.Ok(new { messageResposta = "Notificação enviada com sucesso" });
            });

            app.MapPost("/api/notifications", async (UserModel user, INotificationService notificationServices) =>
            {
                var result = await notificationServices.SendByUserAsync(user);
                return result.HasError 
                    ? Results.BadRequest(result.Errror) 
                    : Results.Ok(new { messageResposta = "Notificação enviada com sucesso" });
            });

            app.Run();
        }
    }
}
