using System;
using System.Collections.Generic;
using System.Linq;
using Auth.Service.Domain.DBModel;
using Auth.Service.Domain.Model;
using Auth.Service.Persistence;
using Newtonsoft.Json;
namespace Auth.Service.Application
{
    public class ClientManger     
    {
       public ClientManger()
        {

        }

        public ClientMaster GetCurrentClient(string clientCode)
        {
            if (string.IsNullOrEmpty(clientCode))
                throw new Exception("client unknown ");
            var client = GetClients(new List<string>() { clientCode }, null).FirstOrDefault();
            return client;
        }

        public List<ClientMaster> GetClients(List<string> domains, List<int> clientIds)
        {
            if (domains == null)
                domains = new List<string>();
            if (clientIds == null)
                clientIds = new List<int>();
            var clients = ClientPersistence.GetClients(domains, clientIds);
            return clients;
        }
    }
}
