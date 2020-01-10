using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
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

            var emailAddress = await sessionStorage.GetItemAsync<string>("emailAddress");
            ClaimsIdentity identity;
            
            if(emailAddress != null)
            {
                identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, emailAddress),
                },  "apiuth_type");
            }
            else
            {
                identity = new ClaimsIdentity();
            }


            var user = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(user));
        }

        public void SetUserAsAuthentitcated(string emailAddress)
        {
            var identity = new ClaimsIdentity(new[]
{
                new Claim(ClaimTypes.Name, emailAddress),
            }, "apiuth_type");

            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public void SetUserAsLoggedOut()
        {
            sessionStorage.RemoveItemAsync("emailAddress");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}
