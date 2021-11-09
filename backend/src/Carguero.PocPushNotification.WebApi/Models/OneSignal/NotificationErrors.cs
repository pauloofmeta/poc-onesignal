using System.Collections.Generic;

namespace Carguero.PocPushNotification.WebApi.Models.OneSignal
{
    public class NotificationErrors
    {
        public IEnumerable<string> Errors { get; set; }
    }
}