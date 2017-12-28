using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BearerAuthentication
{
    internal class BearerSessionManager
    {
        internal static void SaveAccessToken(BearerAuthenticationToken bearerAuthenticationToken)
        {
            HttpContext.Current.Session["TempBearerAuthenticationToken"] = bearerAuthenticationToken;
        }

        internal static void SaveAccessToken(string token)
        {
            var lastToken = GetLastAccessToken();
            lastToken.access_token = token;

            SaveAccessToken(lastToken);
        }

        internal static BearerAuthenticationToken GetLastAccessToken()
        {
            var token = (BearerAuthenticationToken) HttpContext.Current.Session["TempBearerAuthenticationToken"] ?? new BearerAuthenticationToken();
            return token;
        }
    }
}
