using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Service.Domain.Model
{
    public class AppSettings
    {
        public JwtConfig JWTConfig { get; set; }
        //public List<JwtConfig> IssuersConfig { get; set; }
        public string UpdateDateColName { get; set; }
        public string UpdateByColName { get; set; }        
        public string FrontendSecretKey { get; set; }
    }
}
