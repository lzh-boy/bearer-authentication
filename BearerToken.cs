using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BearerAuthentication
{
    public class BearerToken
    {
        public BearerAuthenticationToken GenerateHeaderToken(string userIdentifier, string uid)
        {
            var bearerAuthenticationToken = GenerateToken(userIdentifier, uid);

            BearerSessionManager.SetExpireOnSession(WebConfigSettings.ExpireMinutes);

            return bearerAuthenticationToken;
        }

        public BearerAuthenticationToken GetActiveToken()
        {
            BearerAuthenticationToken retorno = new BearerAuthenticationToken();
            BearerAuthenticationToken token = BearerSessionManager.GetActiveBearerAuthenticationToken();

            retorno.client = BearerCryptoHelper.Decrypt(token.client);
            retorno.uid = token.uid;
            retorno.access_token = token.access_token;

            return retorno;
        }

        internal BearerAuthenticationToken RefreshAccessToken()
        {
            var lastToken = BearerSessionManager.GetActiveBearerAuthenticationToken();
            if (lastToken.access_token == null) return lastToken;

            string userIdentifier = BearerCryptoHelper.Decrypt(lastToken.client);
            return GenerateToken(userIdentifier, lastToken.uid);
        }

        private BearerAuthenticationToken GenerateToken(string userIdentifier, string uid)
        {
            BearerAuthenticationToken bearerAuthenticationToken = new BearerAuthenticationToken();

            string tokenDescriptografado = string.Concat("Bearer", DateTime.Now.Ticks);

            string access_token = BearerCryptoHelper.Encrypt(tokenDescriptografado);
            string client = BearerCryptoHelper.Encrypt(userIdentifier);

            bearerAuthenticationToken.uid = uid;
            bearerAuthenticationToken.client = client;
            bearerAuthenticationToken.access_token = access_token;

            BearerSessionManager.SaveAccessToken(bearerAuthenticationToken);

            return bearerAuthenticationToken;
        }
    }
}
