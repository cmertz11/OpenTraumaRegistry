﻿using Blazored.SessionStorage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using OpenTraumaRegistry.Client;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OpenTraumaRegistry.UI.MD
{
    public class _otrAuthenticationStateProvider : AuthenticationStateProvider
    {
        private ISessionStorageService sessionStorage;
        private ISyncSessionStorageService syncSessionStorage;
        public _otrAuthenticationStateProvider(ISessionStorageService _sessionStorage, ISyncSessionStorageService _syncSessionStorage)
        {
            sessionStorage = _sessionStorage;
            syncSessionStorage = _syncSessionStorage;
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            var _user = await sessionStorage.GetItemAsync<_User>("_otrUser");
            ClaimsIdentity identity;

            if (_user != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Email, _user.Email),
                    new Claim(ClaimTypes.Name, _user.FirstName),
                    new Claim("SystemAdministrator", _user.SystemAdministrator.ToString())
                }, "apiuth_type");

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

        public _User GetUserFromSession()
        {
            return syncSessionStorage.GetItem<_User>("_otrUser");
        }

    }
        public class _otrAuthorizationHandler : AuthorizationHandler<_otrSystemRoleRequirement>
    {

            protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, _otrSystemRoleRequirement requirement)
            {
                bool IsSystemAdministrator = false;

                try
                {
                    IsSystemAdministrator = Convert.ToBoolean(context.User.FindFirst(c => c.Type == requirement.role).Value);
                }
                catch(Exception ex)
                { }
                    
                 
                if (IsSystemAdministrator)
                {
                    context.Succeed(requirement);
                }

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
