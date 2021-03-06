#nullable disable

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KakaoMvcApp.Pages
{
    public class MyClaimsModel : PageModel
    {
        public IDictionary<string, string> AuthProperties { get; set; }
        // public string access_token { get; set;}

        public async void OnGetAsync()
        {
            var authResult = await HttpContext.AuthenticateAsync();
            if (authResult != null)
            {
                AuthProperties = authResult.Properties.Items;
                
            }
        }
    }
}
