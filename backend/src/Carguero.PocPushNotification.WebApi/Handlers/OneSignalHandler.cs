using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Carguero.PocPushNotification.WebApi.Handlers
{
    public class OneSignalHandler: DelegatingHandler
    {
        private readonly IConfiguration _configuration;

        public OneSignalHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = _configuration.GetValue<string>("ONESIGNAL_TOKEN");

            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", token);

            var id = Guid.NewGuid().ToString();
            var msg = $"[{id} - Request]";
            Debug.WriteLine($"{msg}========Start==========");
            Debug.WriteLine($"{msg} {request.Method} {request.RequestUri.PathAndQuery} {request.RequestUri.Scheme}/{request.Version}");
            Debug.WriteLine($"{msg} Host: {request.RequestUri.Scheme}://{request.RequestUri.Host}");

            foreach (var header in request.Headers)
                Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

            if (request.Content != null)
            {
                foreach (var header in request.Content.Headers)
                    Debug.WriteLine($"{msg} {header.Key}: {string.Join(", ", header.Value)}");

                if (request.Content is StringContent || this.IsTextBasedContentType(request.Headers) || this.IsTextBasedContentType(request.Content.Headers))
                {   
                    var result = await request.Content.ReadAsStringAsync();

                    Debug.WriteLine($"{msg} Content:");
                    Debug.WriteLine($"{msg} {string.Join("", result.Cast<char>().Take(255))}...");

                }
            }

            var start = DateTime.Now;
            
            var resp = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            var end = DateTime.Now;

            Debug.WriteLine($"{msg} Duration: {end - start}");
            Debug.WriteLine($"{msg}==========End==========");

            return resp;
        }
        
        readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };
        
        bool IsTextBasedContentType(HttpHeaders headers)
        {
            IEnumerable<string> values;
            if (!headers.TryGetValues("Content-Type", out values))
                return false;
            var header = string.Join(" ", values).ToLowerInvariant();

            return types.Any(t => header.Contains(t));
        }
    }
}