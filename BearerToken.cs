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

        internal BearerAuthenticationToken RefreshAccessToken()
        {
            var lastToken = BearerSessionManager.GetLastAccessToken();

            string userIdentifier = BearerCryptoHelper.Decrypt(lastToken.client);

            return GenerateHeaderToken(userIdentifier, lastToken.uid);
        }
    }
}
