using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


using GraphQL;
using GraphQL.Authorization;
using GraphQL.Server.Transports.AspNetCore;
using GraphQL.Server.Authorization.AspNetCore;

using GraphQL.Validation;
using GraphQL.Types;
using GraphQL.Server;

using Auth.Service.AuthExtensions;

using Auth.Service.Domain.Model;

using Auth.Service.Application;
namespace Auth.Service
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }
        public Startup(
            IConfiguration configuration
            )
        {
            
            this.Configuration = configuration;
        }

  

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTokenAuthentication(Configuration);

            //JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            //{
            //    Formatting = Formatting.Indented,
            //    ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
            //    PreserveReferencesHandling = PreserveReferencesHandling.Objects
            //};



            var schema = new AccountSchema();
            services.TryAddSingleton<ISchema>(s =>
            {

                
                return schema.GraphQLSchema;
            });


            services.AddGraphQLAuth((_, s) =>
            {
                _.AddPolicy("AuthorizedPolicy", p =>
                {
                    p.RequireClaim("Client");
                    //p.RequireClaim("active", "true");
                });
            });

            services.AddGraphQL(options =>
            {
                options.EnableMetrics = false;
          

            })
               
                .AddSystemTextJson()
              .AddUserContextBuilder(context => new GraphQLUserContext { User = context.User, Context = context})
               ;
            ;


            services.AddControllers()
                .AddNewtonsoftJson
               //.AddJsonOptions
               (options =>
               {
                   options.SerializerSettings.DateParseHandling = DateParseHandling.None;
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                   options.SerializerSettings.Formatting = Formatting.Indented;
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Serialize;
                   options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
               });
            ;

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                DateParseHandling = DateParseHandling.None,
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();            
            services.AddTokenAuthentication(Configuration);

         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseHttpsRedirection();

            ////app.UseGraphiQl("/account");
            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            


            var validationRules = app.ApplicationServices.GetServices<IValidationRule>();
            app.Use(async (context, next) =>
            {
                
                    LogApplication.WriteStateLog("Start");
                    if (!context.User.Identity.IsAuthenticated
                    )
                    {

                        if (string.IsNullOrEmpty(context.Request.Headers["Authorization"] + "") && context.Request.Method.ToUpper() == "POST")
                            await next();
                        else await context.ChallengeAsync(JwtBearerDefaults.AuthenticationScheme);
                    }
                    else
                    {
                        await next();
                    }
                
                //await next();
            });

            app.UseGraphQL<ISchema>("/auth");
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllers();
            //});
        }
    }
}
