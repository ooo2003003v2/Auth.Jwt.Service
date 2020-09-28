using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class Lang
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public string Ref { get; set; }
        public bool IsDefaultLang { get; set; }
    }
}
