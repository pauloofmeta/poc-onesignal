using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Carguero.PocPushNotification.WebApi.Interfaces;
using Carguero.PocPushNotification.WebApi.Models;
using Carguero.PocPushNotification.WebApi.Models.OneSignal;
using Microsoft.Extensions.Configuration;
using Refit;

namespace Carguero.PocPushNotification.WebApi.Services
{
    public class NotificationService: INotificationService
    {
        private readonly IOneSignalApi _oneSignalApi;
        private readonly IConfiguration _configuration;

        public NotificationService(IOneSignalApi oneSignalApi, IConfiguration configuration)
        {
            _oneSignalApi = oneSignalApi;
            _configuration = configuration;
        }
        
        public async Task<ResultModel> SendAllAsync()
        {
            var message = BuildMessage("Campanha Serie")
                .SetTitle("Disponível O Fantastico mundo de bob")
                .SetContent("Já esta disponível na plataforma os episodeos do fantastico mundo de bob.")
                .WithAllUsers();
            return await SendNotificationAsync(message);
        }
        
        public Task<ResultModel> SendByUserAsync(UserModel user)
        {
            throw new System.NotImplementedException();
        }

        private async Task<ResultModel> SendNotificationAsync(NotificationMessage message)
        {
            try
            {
                var result = await _oneSignalApi.CreateAsync(message);
                return string.IsNullOrEmpty(result.Id)
                    ? ResultModel.CreateError("Notificação não pode ser enviada!")
                    : ResultModel.CreateSucces();
            }
            catch (ApiException e)
            {
                return await ResultModel.CreateApiExceptionAsync(e);
            }
            catch (Exception e)
            {
                return ResultModel.CreateError($"Ocorreu um erro ao enviar a notificação. {e.Message}");
            }
        }

        private NotificationMessage BuildMessage(string name)
        {
            var appId = _configuration.GetValue<string>("ONESIGNAL_APP_ID");
            
            return new NotificationMessage
            {
                AppId = appId,
                Name = name
            };
        }
    }
}