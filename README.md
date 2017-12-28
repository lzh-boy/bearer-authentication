# bearer-authentication

## Contents
- [Installation](#installation)
- [About](#about)
- [Implementation](#implementation)


### Installation
````powershell
Install-Package BearerAuthentication
````

### About
Bearer authentication is an easy way to implement token authentication on your WebAPI.

## Simple to implement.
### Implementation

First of all you need to add these three keys on your Web.config to have your own crypto keys, I recommend to change the values.
````xml
<add key="BearerAuthentication.Crypto.PasswordHash" value="MYP4SSW0RDH4SH"/>
<add key="BearerAuthentication.Crypto.SaltKey" value="S@LTK3Y"/>
<add key="BearerAuthentication.Crypto.VIKey" value="V1KEY"/>
````

Then in your `FilterConfig.cs` need to add `BearerAuthenticationFilter` 

````c#
public class FilterConfig
{
    public static void RegisterGlobalFilters(GlobalFilterCollection filters)
    {
        filters.Add(new HandleErrorAttribute());
    }

    public static void RegisterWebApiFilters(System.Web.Http.Filters.HttpFilterCollection filters)
    {
        filters.Add(new BearerAuthenticationFilter());
    }
}
````
and you alter `Global.asax.cs` should be like this, adding the Filter, and adding the `Application_PostAuthorizeRequest` method
````c#
protected void Application_Start()
{
    AreaRegistration.RegisterAllAreas();
    GlobalConfiguration.Configure(WebApiConfig.Register);

    //REGISTERING MY FILTERS
    FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
    FilterConfig.RegisterWebApiFilters(GlobalConfiguration.Configuration.Filters);

    RouteConfig.RegisterRoutes(RouteTable.Routes);
}

protected void Application_PostAuthorizeRequest()
{
    HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
}
````

To finish you need to implement the step in which the user logs into the system, then it will look like this

````c#
BearerToken bearerToken = new BearerToken();
bearerToken.GenerateHeaderToken(user.identifier, user.email);
````

Then after this step, the access_token will always be updated with each request, so it is necessary that always send the latest access_token

Ok, now you can be happy with Bearer Authentication =)
