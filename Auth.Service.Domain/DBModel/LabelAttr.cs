using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class LabelAttr
    {
        public int Id { get; set; }
        public string LangCode { get; set; }
        public int LabelId { get; set; }
        public string LabelDesc { get; set; }

        public virtual Label Label { get; set; }
    }
}
