using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Auth.Service.Domain.DBModel;
using Microsoft.EntityFrameworkCore;
namespace Auth.Service.Persistence
{
    public class AccountPersistence
    {
        public static List<Account> GetAccounts(List<int> id, List<string> email)
        {
            List<Account> ao = new List<Account>();
            try
            {
                using (var db = new DBContext())
                {

                    ao = db.Account
                       .Where(p => id.Count == 0 || id.Any(d => d == p.Id))
                       .Where(p => email.Count == 0 || email.Any(d => d == p.Email))
                       .Where(p => p.IsActive == true)
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

        public static List<AccountAdditionalType> AccountAdditionalType(int accountId)
        {
            List<AccountAdditionalType> ao = new List<AccountAdditionalType>();
            try
            {
                using (var db = new DBContext())
                {

                    ao = db.AccountAdditionalType
                       .Where(p => p.AccountId == accountId)
                       .Include(p=>p.AccountAdditionalTypeNavigation)
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
