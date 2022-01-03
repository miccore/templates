using Microsoft.AspNetCore.Authorization;
using System;
namespace JWTAuthentication.Models{
    public class Policies {
        public const string Admin = "Admin";

        public static AuthorizationPolicy AdminPolicy(){
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole(Admin)
                                                   .Build();          
        }

    }
}