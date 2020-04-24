using System;
using System.Collections.Generic;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace OAuth.AuthServer{

    public static class Config{

        public static IEnumerable<ApiResource> ApiResources(){
            return new[] {
                new ApiResource ("oauthApi", "OAuth Api")
            };
        }
        public static IEnumerable<Client> Clients(){
            return new[] {
                new Client {
                    ClientId = "oauthApi",
                    ClientSecrets = new [] { new Secret("secret".Sha256())},
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { "oauthApi" }
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
                }
            };
        }


    }

}