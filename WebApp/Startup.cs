using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;

[assembly: OwinStartup(typeof(WebApp.Startup))]

namespace WebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();

            // Web API configuration and services (Permite Habilitar o Cors)
            config.EnableCors();


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.EnableSwagger(c => {
                c.SingleApiVersion("v1", "WebApp");
                c.IncludeXmlComments(AppDomain.CurrentDomain.BaseDirectory + @"\bin\WebApp.xml");
            });

            app.UseCors(CorsOptions.AllowAll);

            AtivandoAccessToken(app);

            app.UseWebApi(config);
        }

        private void AtivandoAccessToken(IAppBuilder app)
        {
            //configura servidor de token
            var opcoesConfiguracaoToken = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true, //habilita utilização de endereços nao https
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(1),
                Provider = new ProviderDeTokensDeAcesso() // classe de login e senha
            };

            //habilita o uso do token autorization
            app.UseOAuthAuthorizationServer(opcoesConfiguracaoToken);
            //usa BearerAuthentication
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}
