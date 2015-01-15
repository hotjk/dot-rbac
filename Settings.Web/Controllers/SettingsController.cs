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
            var tNodes = TreeService.GetTreeNodes(Constants.TREE_NODE);

            SettingsResponse resp = new SettingsResponse { Client = client, Entries = new List<SettingsEntry>() };
            foreach(var node in nodes)
            {
                if (node.Entries == null || !node.Entries.Any()) continue;

                List<Node> path = new List<Node>(5);
                var tNode = tNodes.FirstOrDefault(n=>n.Data == node.NodeId);
                path.Add(allNodes.FirstOrDefault(n=>n.NodeId == tNode.Data));
                while(tNode.Parent != null)
                {
                    tNode = tNodes.SingleOrDefault(n=>n.Id == tNode.Parent.Value);
                    if (tNode.Data == null) break;
                    path.Add(allNodes.FirstOrDefault(n=>n.NodeId == tNode.Data));
                }
                string strPath = string.Join("/", path.Select(n=>n.Name).Reverse()) + "/";
                
                foreach(var data in node.Entries)
                {
                    resp.Entries.Add(new SettingsEntry{ Path = strPath + data.Key, Value = data.Value});
                }
            }

            return Request.CreateResponse(HttpStatusCode.OK, resp);
        }
    }
}
