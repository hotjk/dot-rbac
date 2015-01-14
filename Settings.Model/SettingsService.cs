﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Settings.Model
{
    public class SettingsService : ISettingsService
    {
        public SettingsService(ISettingsRepository settingsRepository)
        {
            this.SettingsRepository = settingsRepository;
        }
        private ISettingsRepository SettingsRepository { get; set; }

        public bool UpdateNode(Node node)
        {
            node.Entries.ForEach(x => x.NodeId = node.NodeId);
            return SettingsRepository.SaveNode(node);
        }

        public bool DeleteNode(int nodeId, int version)
        {
            return SettingsRepository.DeleteNode(nodeId, version);
        }

        public Node GetNode(int nodeId)
        {
            return SettingsRepository.GetNode(nodeId);
        }

        public IList<Node> GetNodes()
        {
            return null;
        }

        public IList<Node> GetNodes(int client)
        {
            return null;
        }
    }
}
