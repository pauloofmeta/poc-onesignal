using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;

namespace Carguero.PocPushNotification.WebApi.Common
{
    public static class RefitSettingsHelper
    {
        public static RefitSettings GetSettings()
        {
            return new RefitSettings
            {
                ContentSerializer = new SystemTextJsonContentSerializer(
                    new JsonSerializerOptions
                    {
                        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                        PropertyNamingPolicy = new SnakeCaseNamingPolicy()
                    })
            };
        }
    }
}