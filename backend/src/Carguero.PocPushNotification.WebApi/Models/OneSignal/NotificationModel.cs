using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Carguero.PocPushNotification.WebApi.Models.OneSignal
{
    public class NotificationMessage
    {
        public string AppId { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> Headings { get; }
        public IDictionary<string, string> Subtitle { get; private set; }
        public IDictionary<string, string> Contents { get; }
        public List<string> IncludedSegments { get; set; }

        public NotificationMessage()
        {
            Headings = new Dictionary<string, string>();
            Contents = new Dictionary<string, string>();
        }


        public NotificationMessage SetTitle(string value)
        {
            Headings.Add("en", value);
            return this;
        }

        public NotificationMessage SetSubTitle(string value)
        {
            Subtitle ??= new Dictionary<string, string>();
            Subtitle.Add("en", value);
            return this;
        }

        public NotificationMessage SetContent(string value)
        {
            Contents.Add("en", value);
            return this;
        }

        public NotificationMessage WithAllUsers()
        {
            IncludedSegments ??= new List<string>();
            IncludedSegments.Add("Subscribed Users");
            return this;
        }
    }
}