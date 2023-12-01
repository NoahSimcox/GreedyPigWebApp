using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GreedyPigWebApp.Models;
using Microsoft.VisualBasic;

namespace GreedyPigWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Inputs(string gameType, string strategy, int sitDownAfterRound, int currRoll)
    {
        string info = Program.Main(gameType, strategy, sitDownAfterRound, currRoll);
        
        return RedirectToAction("Output", new {info});
    }

    public IActionResult Output(string gameType, string strategy, int sitDownAfterRound, int currScore, float nextExpectedValue, string info)
    {
        ViewData["info"] = info;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}