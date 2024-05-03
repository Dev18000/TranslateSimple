using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace TranslateSimple.Controller
{
    [Route("[controller]/[action]")]
    public class CultureController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult SetCulture(string culture, string redirectionUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)));
            }

            if (redirectionUri == null) redirectionUri = "/";

            return LocalRedirect(redirectionUri);
        }
    }
}
