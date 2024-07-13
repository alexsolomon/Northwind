using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;

namespace Northwind.Web.Authentication
{
    public class CustomAuthenticationHandler : AuthenticationHandler<CustomAuthenticationHandlerOptions>
    {
        //  private readonly ProtectedSessionStorage _sessionStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        public CustomAuthenticationHandler(IOptionsMonitor<CustomAuthenticationHandlerOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IHttpContextAccessor httpContextAccessor,
        IJwtTokenHelper jwtTokenHelper
        ) : base(options, logger, encoder)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtTokenHelper = jwtTokenHelper;
        }
        // Handle Authenticate using jwt stored in cookie
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var cookieValue = Request.Cookies["UserSession"];
                if (string.IsNullOrEmpty(cookieValue))
                    return AuthenticateResult.Fail("Invalid token");
                var claimsPrincipal = _jwtTokenHelper.ValidateToken(cookieValue);
                if (claimsPrincipal != null)
                {
                    RefreshTokenWithNewExpiry(claimsPrincipal);

                    AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Options.Scheme);
                    return await Task.FromResult(AuthenticateResult.Success(ticket));
                }
                else
                {
                    return AuthenticateResult.Fail("Invalid token");
                }

            }
            catch (Exception)
            {
                return await Task.FromResult(AuthenticateResult.Fail("Invalid token"));
            }

        }
        // Handle Authenticate using UserSession stored in cookie
        // protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        // {
        //     try
        //     {
        //         //await Task.Delay(5000);
        //         var cookieValue = Request.Cookies["UserSession"];
        //         if (string.IsNullOrEmpty(cookieValue))
        //             return AuthenticateResult.Fail("Invalid token");
        //         var userSession = JsonSerializer.Deserialize<UserSession>(cookieValue);
        //         if (userSession != null && !string.IsNullOrEmpty(userSession.UserName))
        //         {
        //             var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        //             {
        //                 new Claim(ClaimTypes.Name, userSession.UserName),
        //                 new Claim(ClaimTypes.Role, userSession.Role ?? "")
        //             }, "CustomAuth"));
        //             AuthenticationTicket ticket = new AuthenticationTicket(claimsPrincipal, Options.Scheme);
        //             return await Task.FromResult(AuthenticateResult.Success(ticket));
        //         }
        //         else
        //         {
        //             return AuthenticateResult.Fail("Invalid token");
        //         }

        //     }
        //     catch
        //     {
        //         return await Task.FromResult(AuthenticateResult.Fail("Invalid token"));
        //     }

        // }
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            // Context.Response.Redirect($"login?ReturnUrl={Context.Request.Path.Value}");
            // return Task.CompletedTask;

            await Results.Redirect($"login?ReturnUrl={Context.Request.Path.Value}").ExecuteAsync(Context);
        }
        private void RefreshTokenWithNewExpiry(ClaimsPrincipal claimsPrincipal)
        {
            var newToken = _jwtTokenHelper.GenerateTokenFromClaimsPrincipal(claimsPrincipal);
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddMinutes(10);
            Response.Cookies.Append("UserSession", newToken);
        }
    }
}