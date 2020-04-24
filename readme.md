### DOCS

 This is the project is a template to demonstrate how minimal configuration an ASP.NET Core web API can be Secured using OpenId Connect and OAuth 2.0.

#### AUTHORIZATION FLOW AND GRANTS

identityServer4 Redirect workflow via Implicit Grant.

#### GET STARTED

Restore Packages

```
nuget restore

```

Generate Cert and Key using the following command
```
req -newkey rsa:2048 -nodes -keyout OAuthServer.key -x509 -days 365 -out OAuthServer.cer

```

Creat a pfx

```
pkcs12 -export -in OAuthServer.cer -inkey OAuthServer.key -out OAuthServer.pfx

```

```
cd ./OAuth.AuthServer
```

Add update the appSetting.json accordingly with the .pfx path and password
Start the authentication server

```
dotnet restore
dotnet run build

```
Start the web api project
```
cd ../OAuth.webApi 
dotnet run build

```

Navigate the token endpoint on the Authentication server 

```
POST  http://localhost:5002/connect/token

{
    "client_id" : ""
    "grant_type": "password"
    "username": ""
    "password": ""
    "client_secret": ""
}

```

copy the reponse token and use it to authenticate webApi endpoints


