using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class Account
    {
        public Account()
        {
            AccountAdditionalType = new HashSet<AccountAdditionalType>();
            AccountEditLog = new HashSet<AccountEditLog>();
            AccountLoginLog = new HashSet<AccountLoginLog>();
            RefreshToken = new HashSet<RefreshToken>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Name { get; set; }
        public int AccountTypeId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ChangePassDeadline { get; set; }
        public bool IsRequiredChangePass { get; set; }
        public string Email { get; set; }
        public string OfficeIosCode { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string Code { get; set; }

        public virtual AccountType AccountType { get; set; }
        public virtual ICollection<AccountAdditionalType> AccountAdditionalType { get; set; }
        public virtual ICollection<AccountEditLog> AccountEditLog { get; set; }
        public virtual ICollection<AccountLoginLog> AccountLoginLog { get; set; }
        public virtual ICollection<RefreshToken> RefreshToken { get; set; }
    }
}
