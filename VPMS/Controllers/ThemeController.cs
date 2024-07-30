using Microsoft.AspNetCore.Mvc;

namespace VPMSWeb.Controllers
{
    public class ThemeController : Controller
    {
        public IActionResult Index()
        {
            //String layout = "_Layout";

            //String sTheme = Request.Query["Theme"];
            //if (sTheme == "DarkTheme")
            //{
            //    layout = "_Layout2";
            //}

            //ViewData["Layout"] = $"~/Views/Shared/{layout}.cshtml";

            return View();
        }

        public IActionResult SetThemes(String sTheme)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(1);

            Response.Cookies.Append("theme", sTheme, cookies);

            return Ok();
        }
    }
}
