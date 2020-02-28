using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Testnt.IdentityServer.Common.Attribute;

namespace Testnt.IdentityServer.Pages
{

    [SecurityHeaders]
    [Authorize]
    public class DiagnosticsModel : PageModel
    {
        public AuthenticateResult AuthenticateResult { get; set; }
        public IEnumerable<string> Clients { get; set; } = new List<string>();


        public async Task<IActionResult> OnGet()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress.ToString() };
            if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return NotFound();
            }

            AuthenticateResult = await HttpContext.AuthenticateAsync();
            if (AuthenticateResult.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = AuthenticateResult.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
            return Page();
        }
    }
}
