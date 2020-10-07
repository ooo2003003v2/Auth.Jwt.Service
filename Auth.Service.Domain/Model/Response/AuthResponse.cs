using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using General.Auth.Account.Domain;
namespace Auth.Service.Domain.Model.Response
{
    public class AuthResponse 
    {
        public string accessToken { get; set; }
        public string tokenType { get; set; } = "bearer";
        public int expiresIn { get; set; }
        public string refreshToken { get; set; }
        public DateTime expires { get; set; }
        public DateTime issued { get; set; }
        public AccountModel account { set; get; }
    }
}
