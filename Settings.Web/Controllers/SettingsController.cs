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

        [HttpGet]
        [Route("api/settings/{client}")]
        public HttpResponseMessage Index(string client)
        {
            var aClient = SettingsService.GetClient(client);
            if (aClient == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "客户不存在");
            }

            var nodes = SettingsService.GetNodes(aClient.Nodes);
            var allNodes = SettingsService.GetNodes();
            var tree = TreeService.GetTree(Constants.TREE_NODE);
            var tNodes = TreeService.GetTreeNodes(Constants.TREE_NODE);

            SettingsResponse resp = new SettingsResponse { Client = client, Entries = new List<SettingsEntry>() };
            var path = new List<Grit.Tree.Node>(5);
            foreach(var node in nodes)
            {
                if (node.Entries == null || !node.Entries.Any()) continue;

                path.Clear();
                tree.FindByData(node.NodeId, path);

                string strPath = string.Join("/",
                path.Select(n => allNodes.FirstOrDefault(x => x.NodeId == n.Data))
                    .Select(n => n.Name)) + "/";

                foreach(var data in node.Entries)
                {
                    resp.Entries.Add(new SettingsEntry{ Path = strPath + data.Key, Value = data.Value});
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
