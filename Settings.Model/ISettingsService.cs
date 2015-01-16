﻿using Settings.Client;
using System;
using System.Collections.Generic;

namespace Settings.Model
{
    public interface ISettingsService
    {
        IEnumerable<Node> GetNodes();
        Node GetNode(int nodeId);
        IEnumerable<Node> GetNodes(int[] nodes);
        bool UpdateNode(Node node);
        bool DeleteNode(Node node);

        Client GetClient(int clientId);
        Client GetClient(string name);
        IEnumerable<Client> GetClients();
        bool UpdateClient(Client client);
        bool SaveClientNodes(IEnumerable<Client> clients);
        bool DeleteClient(Client client);
        SettingsResponse GetClientSettings(Client client, Grit.Tree.Node tree);
    }
}