using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Auth.Service.Domain.DBModel;
using Microsoft.EntityFrameworkCore;
namespace Auth.Service.Persistence
{
    public class RefreshTokenPersistence
    {
        public static RefreshToken GetRefreshTokenRecord(string id, int? clientKeyId, bool getAccount)
        {
            RefreshToken token = new RefreshToken();
            try
            {
                using (var db = new DBContext())
                {

                    token = db.RefreshToken
                       //.Where(p=> clientIds.Count == 0 || clientIds.Any(d=>d ==  p.ClientId))
                       .Where(p => p.Id == id )
                       .Where(p=> clientKeyId == null || p.ClientKeyId == clientKeyId)
                       .If(getAccount, p=>p.Include(r => r.Account))
                       .ToList().FirstOrDefault();

                    ;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");

            }
            return token;
        }




    }
}
