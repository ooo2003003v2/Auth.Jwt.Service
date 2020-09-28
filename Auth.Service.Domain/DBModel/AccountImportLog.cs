using System;
using System.Collections.Generic;

namespace Auth.Service.Domain.DBModel
{
    public partial class AccountImportLog
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string LogMsg { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
