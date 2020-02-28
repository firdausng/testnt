using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Testnt.IdentityServer.Areas.Identity.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly IEventService events;

        public LogoutModel(IIdentityServerInteractionService interaction, IEventService events)
        {
            this.interaction = interaction;
            this.events = events;
        }
        public string LogoutId { get; set; }
        public bool ShowLogoutPrompt { get; set; } = true;

        public async Task<IActionResult> OnGetAsync(string logoutId)
        {
            LogoutId = logoutId; 
            ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt;
            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                ShowLogoutPrompt = false;
                return await OnPostAsync(LogoutId);
            }

            var context = await interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                ShowLogoutPrompt = false;
                return await OnPostAsync(LogoutId);
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string logoutId)
        {
            var logout = await interaction.GetLogoutContextAsync(logoutId);
            string ExternalAuthenticationScheme = null;

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            LogoutId = await interaction.CreateLogoutContextAsync();
                        }

                        ExternalAuthenticationScheme = idp;
                    }
                }
            }

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                //await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);

                // raise the logout event
                await events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (ExternalAuthenticationScheme != null)
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                string url = Url.Action("Logout", new { logoutId = LogoutId });

                // this triggers a redirect to the external provider for sign-out
                return SignOut(new AuthenticationProperties { RedirectUri = url }, ExternalAuthenticationScheme);
            }

            var vm = new
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId,
                ExternalAuthenticationScheme = ExternalAuthenticationScheme
            };

            return RedirectToPage("/Account/LoggedOut", vm);
        }
    }
}
