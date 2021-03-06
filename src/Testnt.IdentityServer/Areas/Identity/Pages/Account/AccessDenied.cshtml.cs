using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Testnt.IdentityServer.Models;

namespace Testnt.IdentityServer.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
       
        public StatusMessage StatusMessage { get; set; }

        public void OnGet()
        {
            StatusMessage = new StatusMessage
            {
                Title = "Access Denied",
                SubTitle = "You do not have access to the resource."
            };
        }
    }
}
