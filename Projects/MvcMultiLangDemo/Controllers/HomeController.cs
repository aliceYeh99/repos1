using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMultiLangDemo.Models;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;

namespace MvcMultiLangDemo.Controllers;


public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IStringLocalizer<HomeController> _localizer;


    public HomeController(ILogger<HomeController> logger, 
        IStringLocalizer<HomeController> localizer)
    {
        _logger = logger;
        _localizer = localizer;
    }
    public IActionResult SetLanguage(string culture)
    {
        // 設定 Cookie 來存語系
        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) } // 記住 1 年
        );

        return RedirectToAction("Index");
    }
    public IActionResult Index()
    {
        // string message = _localizer["HelloMessage"];
        // return Content(message);
        return View();
    }

    [HttpGet]
    public string Get()
    {
        return _localizer["HelloMessage"];
    }
    public IActionResult Privacy()
    {
        return View();
        //return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
