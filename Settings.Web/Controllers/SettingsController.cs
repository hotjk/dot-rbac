using Grit.Utility.Security;
using Newtonsoft.Json;
using Settings.Client;
using Settings.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Settings.Web.Controllers
{
    public class SettingsController : ApiController
    {
        public SettingsController(ISettingsService settingsService,
            Grit.Tree.ITreeService treeService)
        {
            this.SettingsService = settingsService;
            this.TreeService = treeService;
        }

        private Grit.Tree.ITreeService TreeService { get; set; }
        private ISettingsService SettingsService  {get;set;}
        [HttpPost]
        [Route("api/settings")]
        public HttpResponseMessage Index( 
            [System.Web.Mvc.ModelBinder(typeof(Grit.Utility.Web.Json.JsonNetModelBinder))] Envelope envelope)
        {
            var client = SettingsService.GetClient(envelope.Id);
            if (client == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Client not found");
            }

            var decrypted = EnvelopeService.PublicDecrypt(envelope, client.PublicKey);
            var req = JsonConvert.DeserializeObject<SettingsRequest>(decrypted);
            if (req.Client != envelope.Id)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid client");
            }

            var tree = TreeService.GetTree(Constants.TREE_NODE);
            SettingsResponse settings = SettingsService.GetClientSettings(client, tree)
                .Filter(req.Pattern);

            string json = JsonConvert.SerializeObject(settings);

            Envelope resp = EnvelopeService.Encrypt(client.Name, json, client.PublicKey);
            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
