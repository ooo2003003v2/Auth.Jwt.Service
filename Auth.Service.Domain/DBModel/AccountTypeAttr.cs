using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountTypeAttr
    {
        public int Id { get; set; }
        public int AccountTypeId { get; set; }
        public string Name { get; set; }
        public string LangCode { get; set; }

        public virtual AccountType AccountType { get; set; }
    }
}
