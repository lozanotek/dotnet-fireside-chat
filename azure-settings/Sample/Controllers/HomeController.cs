using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICertificateService certificateService;

    public HomeController(ILogger<HomeController> logger, ICertificateService certificateService)
    {
        _logger = logger;
        this.certificateService = certificateService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Cert()
    {
        var publicKey = certificateService.GetDefaultPublicKey();
        return Ok(publicKey);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
