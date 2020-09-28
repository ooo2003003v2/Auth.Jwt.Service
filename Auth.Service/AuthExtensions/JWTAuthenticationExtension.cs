using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Auth.Service.Domain.Model;
using Newtonsoft.Json;
namespace Auth.Service.AuthExtensions
{
    public static class JWTAuthenticationExtension
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, IConfiguration config)
        {


            var IssuersConfig = new List<string>();
            var jwtConfig = Singleton.Instance.AppSettings.JWTConfig;
            
            var ValidIssuers = new  List<string>();
            //IssuersConfig.Select(p => { 
            //    ValidIssuers.Add(p.issuer);
            //    return p.signInKey; }).ToList();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                
                

            .AddJwtBearer(x =>
          {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                   
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime= true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.signInKey)),
                    TokenDecryptionKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.encryptKey)),
                    ValidIssuer = jwtConfig.issuer,
                    //ValidIssuer = "localhost:44390",
                    ValidAudience = jwtConfig.aud
                }
                ;
              x.Events = new JwtBearerEvents
              {
                  OnAuthenticationFailed = context =>
                  {
                      if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                      {
                          context.Response.Headers.Add("Token-Expired", "true");
                      }
                      return Task.CompletedTask;
                  }
              };



          })
            
            
            ;

            return services;
        }

    }
}
