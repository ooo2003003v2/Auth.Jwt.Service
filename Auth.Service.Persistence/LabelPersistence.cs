using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Auth.Service.Domain.DBModel;
using General.EF.Core.Function;
using Microsoft.EntityFrameworkCore;

namespace Auth.Service.Persistence
{
   public class LabelPersistence
    {
        
        public static Label GetLabel(string key)
        {
            Label label = new Label();
            try
            {
                using (var db = new DBContext())
                {

                    label = db.Label
                        .Where(p=>p.LabelKey == key)
                        .Include(p=>p.LabelAttr)                        
                        .FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex + "");

            }
            return label;
        }
    }
}
