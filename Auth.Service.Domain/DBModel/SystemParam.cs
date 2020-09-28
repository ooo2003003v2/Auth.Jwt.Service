using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class SystemParam
    {
        public int Id { get; set; }
        public string ParamKey { get; set; }
        public string Value { get; set; }
    }
}
