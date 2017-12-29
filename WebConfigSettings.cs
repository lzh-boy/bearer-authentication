using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BearerAuthentication
{
    internal static class WebConfigSettings
    {
        internal static string PasswordHash
        {
            get
            {
                return ConfigurationManager.AppSettings["BearerAuthentication.Crypto.PasswordHash"].ToString();
            }
        }

        internal static string SaltKey
        {
            get
            {
                return ConfigurationManager.AppSettings["BearerAuthentication.Crypto.SaltKey"].ToString();
            }
        }

        internal static string VIKey
        {
            get
            {
                return ConfigurationManager.AppSettings["BearerAuthentication.Crypto.VIKey"].ToString();
            }
        }

        internal static int ExpireMinutes
        {
            get
            {
                int retorno = 0;

                int.TryParse(ConfigurationManager.AppSettings["BearerAuthentication.ExpireMinutes"], out retorno);

                return retorno;
            }
        }
    }
}
