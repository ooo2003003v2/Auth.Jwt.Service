using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class RefreshToken
    {
        public string Id { get; set; }
        public int AccountId { get; set; }
        public int ClientKeyId { get; set; }
        public DateTime IssuedTime { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string ProtectedTicket { get; set; }

        public virtual Account Account { get; set; }
        public virtual ClientMaster ClientKey { get; set; }
    }
}
