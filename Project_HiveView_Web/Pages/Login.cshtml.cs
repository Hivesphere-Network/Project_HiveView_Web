using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;


namespace Project_HiveView_Web.Pages
{
    public class Login : PageModel
    {
        public async Task OnGet(string redirectUri)
        {
            await HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
} 