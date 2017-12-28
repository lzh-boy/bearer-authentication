using System;

namespace BearerAuthentication
{
    public class BearerAuthenticationToken
    {
        public string access_token { get; set; }
        public string uid { get; set; }
        public string client { get; set; }

        public override bool Equals(object obj)
        {
            BearerAuthenticationToken objToken = (BearerAuthenticationToken)obj;
            return objToken.access_token == access_token && objToken.client == client && objToken.uid == uid;
        }
    }

}