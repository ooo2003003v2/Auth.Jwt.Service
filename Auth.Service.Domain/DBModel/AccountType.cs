using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountType
    {
        public AccountType()
        {
            Account = new HashSet<Account>();
            AccountAdditionalType = new HashSet<AccountAdditionalType>();
            AccountTypeAttr = new HashSet<AccountTypeAttr>();
        }

        public int Id { get; set; }
        public int Level { get; set; }
        public int GroupId { get; set; }
        public bool? IsActive { get; set; }
        public string AccountTypeCode { get; set; }

        public virtual AccountGroup Group { get; set; }
        public virtual ICollection<Account> Account { get; set; }
        public virtual ICollection<AccountAdditionalType> AccountAdditionalType { get; set; }
        public virtual ICollection<AccountTypeAttr> AccountTypeAttr { get; set; }
    }
}
