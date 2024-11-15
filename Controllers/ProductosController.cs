using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.RateLimiting;
using tl2_tp6_2024_nachoNota.Models;

namespace tl2_tp6_2024_nachoNota.Controllers;

public class ProductosController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private ProductosRepository prodRep;

    public ProductosController(ILogger<HomeController> logger)
    {
        _logger = logger;
        prodRep = new ProductosRepository();
    }

    public IActionResult Listar()
    {
        return View(prodRep.GetAll());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new ProductoViewModel());
    }

    [HttpPost]
    public IActionResult Crear(ProductoViewModel productoVM)
    {
        if(!ModelState.IsValid) return RedirectToAction("Listar");

        var producto = new Producto(productoVM);
        prodRep.Create(producto);
        return RedirectToAction("Listar");
    } 

    [HttpGet]
    public IActionResult Modificar(int idProd)
    {
        var producto = prodRep.GetById(idProd);
        return View(new ProductoViewModel(producto));
    }

    [HttpPost]
    public IActionResult Modificar(ProductoViewModel productoView)
    {

        if(!ModelState.IsValid) return RedirectToAction("Listar");

        var producto = new Producto(productoView);
        prodRep.Update(producto);
        
        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Eliminar(int idProd)
    {
        var producto = prodRep.GetById(idProd);
        return View(producto);
    }

    [HttpPost]
    public IActionResult EliminarConfirm(int idProd)
    {    
        prodRep.Delete(idProd);
        return RedirectToAction("Listar");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}