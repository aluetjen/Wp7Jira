using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
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

            var request = HttpWebRequest.CreateHttp(profile.JiraUrl + "/rest/api/2/" + resource);
            byte[] credentialBuffer = new UTF8Encoding().GetBytes(profile.UserName + ":" + profile.Password);
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(credentialBuffer);
            request.BeginGetResponse(s =>
                                         {
                                             try
                                             {
                                                 var response = request.EndGetResponse(s);
                                                 var stream = response.GetResponseStream();
                                                 var serializer = new JsonSerializer();
                                                 var content = serializer.Deserialize<T>(new JsonTextReader(new StreamReader(stream)));

                                                 responseAction(content);
                                             }
                                             catch (Exception)
                                             {
                                                 Bus.Publish(new JiraLostEvent());
                                             }
                                         }, null);
        }
    }
}
