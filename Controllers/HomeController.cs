using System.Diagnostics;
using System.Security.Cryptography.Xml;
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
        
        TempData["strategy"] = strategy;
        TempData["sitDownAfterRound"] = sitDownAfterRound;
        TempData["currScore"] = 0;
        TempData["rolls"] = 0;

        if (Convert.ToChar(gameType) == 'p')
            return RedirectToAction("RecurrInput");

        string info = Program.Main(gameType, strategy, sitDownAfterRound, currRoll);
        
        return RedirectToAction("SimView", new {info});
    }

    public IActionResult RecurrInput(int currRoll)
    {

        string strategy = TempData["strategy"] as string;
        int sitDownAfterRound = (int)TempData["sitDownAfterRound"];
        int currScore = (int)TempData["currScore"];
        int rolls = (int)TempData["rolls"];
        
        string info = "";
        bool goodStrat = bool.Parse(strategy);
        
        currScore += currRoll;
        TempData["currScore"] = currScore;
        rolls++;
        TempData["rolls"] = rolls;
        
        if (currRoll == 5)
        {
            info = "You failed";
            return RedirectToAction("PlayView", new {info});
        }
        
        if (Simulator.GoodStrat(goodStrat, (int)TempData["rolls"], (int)TempData["currScore"], 3.2f))
        {
            info = $"Your current score is {(int)TempData["currScore"]} and your next expected score is {Simulator.NextExpectedScore((int)TempData["currScore"], 3.2f)} therefore you should give up and lock in your points";
            return RedirectToAction("PlayView", new {info});
        }
        else if (Simulator.BadStrat(goodStrat, (int)TempData["rolls"], sitDownAfterRound))
        {
            info = $"Your current score is {(int)TempData["currScore"]} and it has been {sitDownAfterRound} therefore you should give up and lock in your points";
            return RedirectToAction("PlayView", new {info});
        }

        return RedirectToAction("PlayView", new{info});
    }

    public IActionResult SimView(string info)
    {
        ViewData["simResult"] = info;
        return View();
    }
    public IActionResult PlayView(string? info)
    {

        if (info != null && info.Length > 0)
        {
            TempData["currScore"] = 0;
            TempData["rolls"] = 1;
        }

        ViewData["playResult"] = info;
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}