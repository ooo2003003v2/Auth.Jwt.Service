using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Service.Domain.Model.Request
{
    public class LoginInput
    {
        public string login { get; set; }
        public string password{ get; set; }
    }
}
