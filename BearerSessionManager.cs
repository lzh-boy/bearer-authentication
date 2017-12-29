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
            var lastToken = GetActiveBearerAuthenticationToken();
            lastToken.access_token = token;

            SaveAccessToken(lastToken);
        }

        internal static BearerAuthenticationToken GetActiveBearerAuthenticationToken()
        {
            var token = (BearerAuthenticationToken) HttpContext.Current.Session["TempBearerAuthenticationToken"] ?? new BearerAuthenticationToken();
            return token;
        }

        internal static void SetExpireOnSession(int expiryMinutes)
        {
            HttpContext.Current.Session["TempBearerAuthenticationExpiry"] = DateTime.Now.AddMinutes(expiryMinutes);
        }

        internal static DateTime? GetExpireOnSession()
        {
            return (DateTime?)HttpContext.Current.Session["TempBearerAuthenticationExpiry"];
        }
    }
}
