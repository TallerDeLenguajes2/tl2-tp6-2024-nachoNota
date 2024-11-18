using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.RateLimiting;
using tl2_tp6_2024_nachoNota.Models;

namespace tl2_tp6_2024_nachoNota.Controllers;

public class ProductosController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IProductosRepository _prodRep;

    public ProductosController(ILogger<HomeController> logger, IProductosRepository _prodRep)
    {
        _logger = logger;
        this._prodRep = _prodRep;
    }

    public IActionResult Listar()
    {
        ViewData["RolUsuario"] = HttpContext.Session.GetString("NivelAcceso"); // paso la info del rol a la vista
        return View(_prodRep.GetAll());
    }

    [HttpGet]
    [RolRequerido(Rol.Administrador)]
    public IActionResult Crear()
    {
        return View(new ProductoViewModel());
    }

    [HttpPost]
    [RolRequerido(Rol.Administrador)]
    public IActionResult Crear(ProductoViewModel productoVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Listar");

        var producto = new Producto(productoVM);
        _prodRep.Create(producto);
        return RedirectToAction("Listar");
    } 

    [HttpGet]
    [RolRequerido(Rol.Administrador)]
    public IActionResult Modificar(int idProd)
    {
        var producto = _prodRep.GetById(idProd);
        return View(new ProductoViewModel(producto));
    }

    [HttpPost]
    [RolRequerido(Rol.Administrador)]
    public IActionResult Modificar(ProductoViewModel productoView)
    {

        if(!ModelState.IsValid) return RedirectToAction("Listar");

        var producto = new Producto(productoView);
        _prodRep.Update(producto);
        
        return RedirectToAction("Listar");
    }

    [HttpGet]
    [RolRequerido(Rol.Administrador)]
    public IActionResult Eliminar(int idProd)
    {
        var producto = _prodRep.GetById(idProd);
        return View(producto);
    }

    [HttpPost]
    [RolRequerido(Rol.Administrador)]
    public IActionResult EliminarConfirm(int idProd)
    {    
        _prodRep.Delete(idProd);
        return RedirectToAction("Listar");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}