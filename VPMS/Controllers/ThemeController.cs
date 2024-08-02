using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SetThemes(String data)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("theme", data, cookies);

            return Ok();
        }
    }
}
