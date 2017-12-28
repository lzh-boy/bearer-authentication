# bearer-authentication
### Is simple to implement on your WebAPI.
First of all you need to add 3 keys on your Web.config and customize it:
````xml
<add key="BearerAuthentication.Crypto.PasswordHash" value="MYP4SSW0RDH4SH"/>
<add key="BearerAuthentication.Crypto.SaltKey" value="S@LTK3Y"/>
<add key="BearerAuthentication.Crypto.VIKey" value="V1KEY"/>
````
