// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiResource> Apis =>
            new List<ApiResource>
            {
                new ApiResource("IssueTrackerApi", "Issue Tracker")
            };
        
        public static IEnumerable<Client> Clients =>
            new List<Client> 
            {
                new Client
                {
                    ClientId = "IssueTrackerSpa",
                    ClientName = "Issue Tracker Spa",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,

                    RedirectUris = { "http://localhost:5002/login/callback" },
                    PostLogoutRedirectUris = { "http://localhost:5002/logout/callback" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "IssueTrackerApi"
                    }
                }
            };
        
    }
}