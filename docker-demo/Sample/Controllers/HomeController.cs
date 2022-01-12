using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Sample.Models;

namespace Sample.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        var process = Process.GetCurrentProcess().ProcessName;
        var hostName = System.Net.Dns.GetHostName();

        var frameworkDesc = RuntimeInformation.FrameworkDescription;
        var procArch = RuntimeInformation.ProcessArchitecture.ToString();
        var osDesc = RuntimeInformation.OSDescription;
        var containerized = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") is object ? "true" : "false";
        var cpuCores = Environment.ProcessorCount;

        var opsData = new Dictionary<string, string>
        {
            { "Process", process },
            { "Hostname", hostName },
            { "Framework", frameworkDesc },
            { "ProcessArch", procArch },
            { "OS", osDesc },
            { "Containerized", containerized },
            { "CPUCores", cpuCores.ToString() }
        };

        return View(opsData);
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
