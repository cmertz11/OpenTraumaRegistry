using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using OpenTraumaRegistry.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.UI.MD
{
    public class _otrAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService sessionStorage;
        public _otrAuthenticationStateProvider(ISessionStorageService _sessionStorage)
        {
            sessionStorage = _sessionStorage;
        }

        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var _user = await sessionStorage.GetItemAsync<_User>("_otrUser");
            ClaimsIdentity identity;
            
            if(_user != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.Name, _user.FirstName),
                    new Claim("SystemAdministrator", _user.SystemAdministrator.ToString())
                },  "apiuth_type");

            }
            else
            {
                identity = new ClaimsIdentity();
            }


            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public async Task SetUserAsAuthentitcatedAsync()
        {

            var _user = await sessionStorage.GetItemAsync<_User>("_otrUser");
            ClaimsIdentity identity; 
            identity = new ClaimsIdentity(new[]
{
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.Name, _user.FirstName),
                    new Claim("SystemAdministrator", _user.SystemAdministrator.ToString())
                }, "apiuth_type");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void SetUserAsLoggedOut()
        {
            sessionStorage.RemoveItemAsync("_otrUser");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
 

    public class _otrAuthorizationHandler : AuthorizationHandler<_otrSystemRoleRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, _otrSystemRoleRequirement requirement)
        {
            //if (!context.User.HasClaim(c => c.Type == ClaimTypes.Email))
            //{
            //    return Task.CompletedTask;
            //}

            //var emailAddress = context.User.FindFirst(c => c.Type == ClaimTypes.Email).Value;

            //if (emailAddress.EndsWith(requirement.CompanyDomain))
            //{
            //    return context.Succeed(requirement);
            //}

            return Task.CompletedTask;
        }
    }

    public class _otrSystemRoleRequirement : IAuthorizationRequirement
    {
        public string role { get; }

        public _otrSystemRoleRequirement(string _role)
        {
            role = _role;
        }
    }
}
