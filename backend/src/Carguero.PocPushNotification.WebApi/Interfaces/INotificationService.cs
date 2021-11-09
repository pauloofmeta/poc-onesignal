using System.Threading.Tasks;
using Carguero.PocPushNotification.WebApi.Models;

namespace Carguero.PocPushNotification.WebApi.Interfaces
{
    public interface INotificationService
    {
        Task<ResultModel> SendAllAsync();
        Task<ResultModel> SendByUserAsync(UserModel user);
    }
}