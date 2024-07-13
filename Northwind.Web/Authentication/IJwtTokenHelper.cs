using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Northwind.Web.Authentication
{
    public interface IJwtTokenHelper
    {
        string GenerateToken(UserSession userSession);
        ClaimsPrincipal ValidateToken(string authToken);
        string GenerateTokenFromClaimsPrincipal(ClaimsPrincipal claimsPrincipal);
    }
}