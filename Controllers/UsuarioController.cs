using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.RateLimiting;
using tl2_tp6_2024_nachoNota.Models;

namespace tl2_tp6_2024_nachoNota.Controllers;

public class UsuarioController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUsuarioRepository _usuRep;

    public UsuarioController(ILogger<HomeController> logger, IUsuarioRepository usuRep)
    {
        _logger = logger;
        _usuRep = usuRep;
    }

    public IActionResult Index(){
        return View(new UsuarioViewModel());
    }

    

}