using System.Threading.Tasks;
using Carguero.PocPushNotification.WebApi.Models.OneSignal;
using Refit;

namespace Carguero.PocPushNotification.WebApi.Interfaces
{
    public interface IOneSignalApi
    {
        [Post("/v1/notifications")]
        Task<NotificationResult> CreateAsync(NotificationMessage message);
    }
}