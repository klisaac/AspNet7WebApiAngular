using System;
using System.Collections.Generic;
using System.Security.Claims;
using AspNet5.Core.Configuration;

namespace AspNet5.Integration.Tests.Infrastructure
{
    public class ClaimsProviderTest
    {
        public ClaimsProviderTest()
        {
            Claims = new List<Claim>();
        }
        public ClaimsProviderTest(IList<Claim> claims)
        {
            Claims = claims;
        }

        public IList<Claim> Claims { get; }

        public static ClaimsProviderTest WithAdminClaims()
        {
            var provider = new ClaimsProviderTest();
            provider.Claims.Add(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            provider.Claims.Add(new Claim(ClaimTypes.Name, "testUser"));
            provider.Claims.Add(new Claim(ClaimTypes.Role, Role.Admin));
            return provider;
        }
        public static ClaimsProviderTest WithUserClaims()
        {
            var provider = new ClaimsProviderTest();
            provider.Claims.Add(new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()));
            provider.Claims.Add(new Claim(ClaimTypes.Name, "testUser"));
            provider.Claims.Add(new Claim(ClaimTypes.Role, Role.User));
            return provider;
        }

        //public static ClaimsProviderTest WithAdminTokenClaims()
        //{
        //    var provider = new ClaimsProviderTest();
        //    provider.Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, "testAdmin"));
        //    provider.Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //    provider.Claims.Add(new Claim(JwtRegisteredClaimNames.UniqueName, "testAdmin"));
        //    provider.Claims.Add(new Claim(ClaimTypes.Name, "testAdmin"));
        //    provider.Claims.Add(new Claim(ClaimTypes.Role, Role.Admin));
        //    return provider;
        //}
    }
}