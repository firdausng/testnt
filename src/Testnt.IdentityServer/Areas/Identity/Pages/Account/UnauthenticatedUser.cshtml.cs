using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Testnt.IdentityServer.Models;

namespace Testnt.IdentityServer.Areas.Identity.Pages.Account
{
    public class UnauthenticatedUserModel : PageModel
    {
        public StatusMessage StatusMessage { get; set; }

        public void OnGet()
        {
            StatusMessage = new StatusMessage
            {
                Title = "Failed to logged you in",
                SubTitle = "Something wrong with your Id, contact us for more detail"
            };
        }
    }
}
