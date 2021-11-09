using System.Text.Json;
using Carguero.PocPushNotification.WebApi.Extensions;

namespace Carguero.PocPushNotification.WebApi.Common
{
    public class SnakeCaseNamingPolicy: JsonNamingPolicy
    {
        public override string ConvertName(string name) =>
            name.ToSnakeCase();
    }
}