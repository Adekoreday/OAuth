using System;
using System.Collections.Generic;
using System.Diagnostics;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.Extensions.Configuration;

namespace OAuth.AuthServer{

    public static class Config{

        public static IEnumerable<ApiResource> ApiResources(){
            return new[] {
                new ApiResource ("oauthApi", "OAuth Api")
            };
        }

       public static IEnumerable<IdentityResource> IdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> Clients(IConfiguration _configuration){


            var oauthWebRedirectUrl = _configuration.GetSection("Urls:OAuthWeb:SiginIn").Value;
            var oauthWebLogoutRedirectUrl = _configuration.GetSection("Urls:OAuthWeb:SiginOut").Value;

            Debug.Assert(!String.IsNullOrEmpty(oauthWebRedirectUrl), "redirect url cannot be empty");
            Debug.Assert(!String.IsNullOrEmpty(oauthWebLogoutRedirectUrl), "logout redirect url cannot be empty");
            return new[] {
                new Client {
                    ClientId = "oauthApi",
                    ClientSecrets = new [] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "oauthApi" }
                },
                new Client {
                    ClientId = "oauthWeb",
                    ClientSecrets = new [] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = new [] {oauthWebRedirectUrl},
                    PostLogoutRedirectUris = new [] {oauthWebLogoutRedirectUrl},
                    AllowedScopes = { 
                          IdentityServerConstants.StandardScopes.OpenId, //OpenIdConnect scopes
                          IdentityServerConstants.StandardScopes.Profile,
                         "oauthApi" //resource scopes
                        },
                    AllowOfflineAccess = true
                }
            };
        }
        public static IEnumerable<TestUser> TestUsers()
        {
            return new[]
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "adeyemi@mailinator.com",
                    Password = "Password"
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "adeyemi",
                    Password = "Password"
                }
            };
        }


    }

}