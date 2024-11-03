using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.RateLimiting;
using tl2_tp6_2024_nachoNota.Models;

namespace tl2_tp6_2024_nachoNota.Controllers;

public class PresupuestosController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private PresupuestosRepository presRep;

    public PresupuestosController(ILogger<HomeController> logger)
    {
        _logger = logger;
        presRep = new PresupuestosRepository();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}