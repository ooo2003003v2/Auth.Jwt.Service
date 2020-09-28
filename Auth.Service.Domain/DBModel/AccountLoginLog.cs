using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountLoginLog
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string UserAgent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string AccessIp { get; set; }

        public virtual Account Account { get; set; }
    }
}
