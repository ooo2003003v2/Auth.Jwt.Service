using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class Label
    {
        public Label()
        {
            LabelAttr = new HashSet<LabelAttr>();
        }

        public int Id { get; set; }
        public string LabelKey { get; set; }
        public bool IsPrestore { get; set; }

        public virtual ICollection<LabelAttr> LabelAttr { get; set; }
    }
}
