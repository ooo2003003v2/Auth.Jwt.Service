using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountEditLog
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string Json { get; set; }
        public string Remark { get; set; }
        public string ActionCode { get; set; }

        public virtual Account Account { get; set; }
    }
}
