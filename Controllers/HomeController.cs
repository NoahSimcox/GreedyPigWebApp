using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GreedyPigWebApp.Models;

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
        
        return RedirectToAction("PlayGame", new {gameType = gameType, strategy = strategy, sitDownAfterRound = sitDownAfterRound});
    }

    public IActionResult PlayGame(string gameType, string strategy, int sitDownAfterRound)
    {
        ViewData["Game Type Result: "] = gameType;
        ViewData["strategy Result: "] = strategy;
        ViewData["Sit Down After Round Result: "] = sitDownAfterRound;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}