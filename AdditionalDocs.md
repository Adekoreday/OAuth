#### Project Architecture

The project contains the following

- dotnet core web Api
- dotnet core mvc
- dotnet core test project
- The domain dotnet standard library
- The dotnet data standard library

The Choosen Auth flow for each project

The web API project implements the identityServer4 Credential workflow via ResourceOwnerPasswordAndClientCredentials.

- LOGIN
The api endpoint user is expected to send the username and password to the identity server for authentication via the **Resource Password Credentials flow**.

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


- SIGN UP

The user is expected to signup using the client **credential auth flow**
The api endpoint expected to recieve a clientId and secret and a token which would enable such user perfom transaction like account creation.

Upon creation of account such user is expected to signin again using the **Resource Password Credentials flow**. and recieve a token that contains all header, claims and signature.

Here we send this first

```
POST  http://localhost:5002/connect/token

{
    "client_id" : ""
    "grant_type": "password"
    "client_secret": ""
}

```


Then create a user then send this again for full access.

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

#### ASP.NET WEBPROJECT

The ASP.NET web project represent a basic client side application which wants to implement OAuth2 and openId connect.

Here is the following

Implicit flow
The Authorization Code flow
The better approach is using Hybrid flow 

we'll be implementing the implicit flow

First add

```
dotnet add package Microsoft.AspNetCore.Authentication.Cookies --version 2.2.0
```

Then add

```
dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect

```

update the startup.cs of the MVC project with the openIdConnect Middleware

```

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            services.AddAuthentication(options =>
            {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
            })
            .AddCookie("Cookies")
           .AddOpenIdConnect("oidc", options =>
            {
                options.Authority = "http://localhost:5002";
                options.ClientId = "oauthWeb";
                options.SignInScheme = "Cookies";
                options.SaveTokens = true;
                options.RequireHttpsMetadata = false;
                options.ResponseType = "id_token";
            });

```

Then add [Authorize] attribute to the protected endpoint

