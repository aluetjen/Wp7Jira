using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using Aluetjen.Infrastructure;
using Aluetjen.Jira.Contexts.Import.Events;
using Aluetjen.Jira.Contexts.Import.ViewModel;
using Newtonsoft.Json;

namespace Aluetjen.Jira.Contexts.Import.Gateway.Jira
{
    public class JiraService : IJiraService
    {
        public IBus Bus { get; set; }
        public IDocumentStore Store { get; set; }

        public void Get<T>(string resource, Action<T> responseAction)
        {
            if (!Store.Exists<Profile>("Profile")) throw new UnauthorizedAccessException();

            var profile = Store.Load<Profile>("Profile");

            var requestUriString = profile.JiraUrl + "/rest/api/2/" + resource;

            Debug.WriteLine("Jira: Requesting {0}", requestUriString);

            var request = WebRequest.CreateHttp(requestUriString);
            if (request.Headers == null)
            {
                request.Headers = new WebHeaderCollection();
            }
            request.Headers[HttpRequestHeader.IfModifiedSince] = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);

            byte[] credentialBuffer = new UTF8Encoding().GetBytes(profile.UserName + ":" + profile.Password);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);
            request.BeginGetResponse(s => ProcessResponse(responseAction, request, s), null);
        }

        private void ProcessResponse<T>(Action<T> responseAction, HttpWebRequest request, IAsyncResult s)
        {
            try
            {
                using (var response = request.EndGetResponse(s))
                {
                    var stream = response.GetResponseStream();
                    var serializer = new JsonSerializer();

                    using(var reader = new StreamReader(stream))
                    {
#if (DEBUG)
                        var responseText = reader.ReadToEnd();
                        Debug.WriteLine("Jira: response {0}", responseText);

                        var content = serializer.Deserialize<T>(new JsonTextReader(new StringReader(responseText)));
#else
                        var content = serializer.Deserialize<T>(new JsonTextReader(reader));
#endif

                        responseAction(content);
                    }
                }
            }
            catch (Exception)
            {
                Bus.Publish(new JiraLostEvent());
            }
        }
    }
}
