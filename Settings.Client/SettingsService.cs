using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using Grit.Utility.Security;
using System.Net.Http.Headers;

namespace Settings.Client
{
    public class SettingsService
    {
        public ClientSettings GetClientSettings(string client, string api, string path, string privateKey)
        {
            ClientSettingsRequest csr = new ClientSettingsRequest(client, path);
            string json = JsonConvert.SerializeObject(csr);
            var reqEnvelope = EnvelopeService.PrivateEncrypt(client, json, privateKey);
            var reqContent = JsonConvert.SerializeObject(reqEnvelope);
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(api);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpRequestMessage req = new HttpRequestMessage(HttpMethod.Post, "");
                req.Content = new StringContent(reqContent, Encoding.UTF8, "application/json");
                var response = httpClient.SendAsync(req).Result;
                if(!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var content = response.Content.ReadAsStringAsync().Result;
                var envelope = JsonConvert.DeserializeObject<Envelope>(content);
                string decrypted = EnvelopeService.Decrypt(envelope, privateKey);
                return JsonConvert.DeserializeObject<ClientSettings>(decrypted);
            }
        }
    }
}
