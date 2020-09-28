using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class TokenPayload
    {
        public string MidToken { get; set; }
        public string Ip { get; set; }
        public string JwtToken { get; set; }
        public DateTime LastAccess { get; set; }
    }
}
