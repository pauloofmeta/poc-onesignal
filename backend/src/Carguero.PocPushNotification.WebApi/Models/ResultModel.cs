using System;
using System.Net;
using System.Threading.Tasks;
using Carguero.PocPushNotification.WebApi.Models.OneSignal;
using Refit;

namespace Carguero.PocPushNotification.WebApi.Models
{
    public class ResultModel
    {
        public bool Sucess { get; private set; }
        public string Errror { get; private set; }
        public bool HasError => !Sucess;

        public static ResultModel CreateSucces() =>
            new ResultModel
            {
                Sucess = true
            };

        public static ResultModel CreateError(string error)=>
            new ResultModel
            {
                Sucess = false,
                Errror = error
            };

        public static async Task<ResultModel> CreateApiExceptionAsync(ApiException e)
        {
            var error = $"Ocorreu um erro ao enviar a notificação. {e.Message}";
            if (e.HasContent)
            {
                if (e.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errors = await e.GetContentAsAsync<NotificationErrors>();
                    error = string.Join(", ", errors.Errors);
                }
                else
                    error = $"Ocorreu um erro ao enviar a notificação. {e.Content}";
            }

            return new ResultModel
            {
                Sucess = false,
                Errror = error
            };
        }
    }
}