using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class ClientMaster
    {
        public ClientMaster()
        {
            RefreshToken = new HashSet<RefreshToken>();
        }

        public int ClientKeyId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientName { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
        public string ClientKey { get; set; }

        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
