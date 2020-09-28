using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Service.Domain.Model
{
    public class JwtConfig
    {

        
        public string encryptKey { get; set; }
        public string signInKey { get; set; }
        public string aud { get; set; }
        public string issuer { get; set; }
        public int expireIn  { get; set; }
    }
}
