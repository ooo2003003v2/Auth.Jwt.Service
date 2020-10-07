using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Auth.Service.Domain.DBModel;
using Microsoft.EntityFrameworkCore;
using General.EF.Core.Function;
namespace Auth.Service.Persistence
{
    public class ClientPersistence
    {
        public static List<ClientMaster> GetClients(List<string> clientKey, List<int> clientKeyIds)
        {
            List<ClientMaster> ao = new List<ClientMaster>();
            try
            {
                using (var db = new DBContext())
                {

                    ao = db.ClientMaster
                       .Where(p=> clientKeyIds.Count == 0 || clientKeyIds.Any(d=>d ==  p.ClientKeyId))
                       .Where(p => clientKey.Count == 0 || clientKey.Any(d => d == p.ClientKey))                       
                       .Where(p => p.Active == true)
                       .ToList();

                    ;                

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");

            }
            return ao;
        }



    }
}
