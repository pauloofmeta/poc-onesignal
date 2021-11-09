using System.Linq;

namespace Carguero.PocPushNotification.WebApi.Extensions
{
    public static class StringExtensions
    {
        public static string ToSnakeCase(this string str) =>
            string.Concat((str ?? string.Empty)
                    .Select((x, i) => i > 0 && char.IsUpper(x) &&
                                      !char.IsUpper(str[i-1]) ? $"_{x}" : x.ToString())
                )
                .ToLower();
    }
}