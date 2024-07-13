using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Northwind.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // private readonly ProtectedSessionStorage _sessionStorage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJwtTokenHelper _jwtTokenHelper;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(IHttpContextAccessor httpContextAccessor,
        IJwtTokenHelper jwtTokenHelper)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtTokenHelper = jwtTokenHelper;
        }

        // Below impelmentation writes jwt in cookie

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                //await Task.Delay(5000);
                var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["UserSession"];
                if (string.IsNullOrEmpty(cookieValue))
                    return await Task.FromResult(new AuthenticationState(_anonymous));
                var claimsPrincipal = _jwtTokenHelper.ValidateToken(cookieValue);
                if (claimsPrincipal != null)
                {
                    return await Task.FromResult(new AuthenticationState(claimsPrincipal));
                }
                return await Task.FromResult(new AuthenticationState(_anonymous));

            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null && !string.IsNullOrEmpty(userSession.UserName))
            {
                // await _sessionStorage.SetAsync("UserSession", userSession);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(10);

                var jwtToken = _jwtTokenHelper.GenerateToken(userSession);
                _httpContextAccessor.HttpContext?.Response.Cookies.Append("UserSession", _jwtTokenHelper.GenerateToken(userSession), options);
                claimsPrincipal = _jwtTokenHelper.ValidateToken(jwtToken);
            }
            else
            {
                _httpContextAccessor.HttpContext?.Response.Cookies.Delete("UserSession");

                // await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            var authenticationState = new AuthenticationState(claimsPrincipal);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

            await Task.CompletedTask;
        }

        // Below implementation directly writes userSession in cookie

        // public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        // {
        //     try
        //     {
        //         //await Task.Delay(5000);
        //         var cookieValue = _httpContextAccessor.HttpContext?.Request.Cookies["UserSession"];
        //         if (string.IsNullOrEmpty(cookieValue))
        //             return await Task.FromResult(new AuthenticationState(_anonymous));
        //         var userSession = JsonSerializer.Deserialize<UserSession>(cookieValue);
        //         if (userSession != null && !string.IsNullOrEmpty(userSession.UserName))
        //         {
        //             var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        //             {
        //                 new Claim(ClaimTypes.Name, userSession.UserName),
        //                 new Claim(ClaimTypes.Role, userSession.Role ?? "")
        //             }, "CustomAuth"));
        //             return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        //         }
        //         return await Task.FromResult(new AuthenticationState(_anonymous));

        //     }
        //     catch
        //     {
        //         return await Task.FromResult(new AuthenticationState(_anonymous));
        //     }
        // }

        // public async Task UpdateAuthenticationState(UserSession userSession)
        // {
        //     ClaimsPrincipal claimsPrincipal;

        //     if (userSession != null && !string.IsNullOrEmpty(userSession.UserName))
        //     {
        //         // await _sessionStorage.SetAsync("UserSession", userSession);
        //         CookieOptions options = new CookieOptions();
        //         options.Expires = DateTime.Now.AddMinutes(1);
        //         _httpContextAccessor.HttpContext?.Response.Cookies.Append("UserSession", JsonSerializer.Serialize(userSession), options);
        //         claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name, userSession.UserName),
        //             new Claim(ClaimTypes.Role, userSession.Role ?? "")
        //         }, "CustomAuth"));
        //     }
        //     else
        //     {
        //         CookieOptions options = new CookieOptions();
        //         options.Expires = DateTime.Now.AddMinutes(1);
        //         _httpContextAccessor.HttpContext?.Response.Cookies.Append("UserSession", "", options);

        //         // await _sessionStorage.DeleteAsync("UserSession");
        //         claimsPrincipal = _anonymous;
        //     }

        //     var authenticationState = new AuthenticationState(claimsPrincipal);
        //     NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));

        //     await Task.CompletedTask;
        // }
    }
}