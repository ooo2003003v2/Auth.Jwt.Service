using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountGroupAttr
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public string LangCode { get; set; }
        public int GroupId { get; set; }

        public virtual AccountGroup Group { get; set; }
    }
}
