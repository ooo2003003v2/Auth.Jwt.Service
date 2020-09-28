using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountGroup
    {
        public AccountGroup()
        {
            AccountGroupAttr = new HashSet<AccountGroupAttr>();
            AccountType = new HashSet<AccountType>();
        }

        public int Id { get; set; }
        public string GroupCode { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<AccountGroupAttr> AccountGroupAttr { get; set; }
        public virtual ICollection<AccountType> AccountType { get; set; }
    }
}
