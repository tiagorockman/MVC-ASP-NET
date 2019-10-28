using Microsoft.Owin.Security.OAuth;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //validação ou não do usuario
            var usuario = BaseUsuarios
                .Usuarios()
                .FirstOrDefault(x => x.Nome == context.UserName
                                && x.Senha == context.Password);

            if(usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado ou senha incorreta");
            }

            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);
        }
    }
}