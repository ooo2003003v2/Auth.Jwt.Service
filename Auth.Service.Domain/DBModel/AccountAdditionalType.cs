using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountAdditionalType
    {
        public int Id { get; set; }
        public int AccountAdditionalTypeId { get; set; }
        public int AccountId { get; set; }

        public virtual Account Account { get; set; }
        public virtual AccountType AccountAdditionalTypeNavigation { get; set; }
    }
}
