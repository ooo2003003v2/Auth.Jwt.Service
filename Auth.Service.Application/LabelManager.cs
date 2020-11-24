using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Auth.Service.Persistence;
using Auth.Service.Domain.Model.Request;
using Auth.Service.Domain.Model;
using Auth.Service.Domain.DBModel;
using System.Security.Claims;
using Newtonsoft.Json;
using General.Auth.Account.Domain;
using General.EF.Core.Function;
namespace Auth.Service.Application
{
    public class LabelManager
    {


        

        public static string GetLabel(string key)
        {
            return LabelPersistence.GetLabel(key)?.LabelAttr.FirstOrDefault()?.LabelDesc;
        }


    }
}
