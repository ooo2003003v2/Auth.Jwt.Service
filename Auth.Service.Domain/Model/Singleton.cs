using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Auth.Service.Domain.Model
{
    public class Singleton
    {
        private static readonly Lazy<Singleton>
            lazy =
            new Lazy<Singleton>
                (() => new Singleton());
        private HttpContextAccessor _context;

        private AppSettings _appSettings;

        

        public static Singleton Instance { get { return lazy.Value; } }

        public HttpContextAccessor Context { get { return _context; } }

        public AppSettings AppSettings { get { return _appSettings; } }

        

        //public AppSettings AppSettings { get { return _appSettings; } }
        private Singleton()
        {

            _appSettings = JsonConvert.DeserializeObject<AppSettings>(
                JsonConvert.SerializeObject(
                    JsonConvert.DeserializeObject<JObject>(
                (File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json")))["AppSettings"]
                )
                );
            
            _context = new HttpContextAccessor();
        }
    }
}
