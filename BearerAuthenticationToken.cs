using System;
using System.Collections.Generic;

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

        public override int GetHashCode()
        {
            var hashCode = -2047944804;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(access_token);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(uid);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(client);
            return hashCode;
        }
    }

}