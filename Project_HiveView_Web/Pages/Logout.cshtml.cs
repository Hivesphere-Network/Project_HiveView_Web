using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Project_HiveView_Web.Pages
{
    public class Logout : PageModel
    {
        [Authorize]
        public async Task OnGet()
        {
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri("/")
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}