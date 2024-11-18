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

    public IActionResult Login(UsuarioViewModel usuarioVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Index");

        var usuario = _usuRep.GetUsuario(usuarioVM.NombreUsuario, usuarioVM.Contrasena); 
        if(usuario is null) return RedirectToAction("Index");

        loguearUsuario(usuario);

       return RedirectToRoute(new { controller = "Home", action = "Index" });
    }

    public IActionResult Logout()
    {
        Response.Cookies.Delete("AuthCookie");
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    private void loguearUsuario(Usuario user)
    {
        HttpContext.Session.SetString("Usuario", user.NombreUsuario);
        HttpContext.Session.SetString("NivelAcceso", user.Rol.ToString());
    }
    

}