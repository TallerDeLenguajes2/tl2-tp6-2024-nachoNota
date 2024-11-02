using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
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

        return View(prodRep.listarProductos());
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new Producto());
    }

    [HttpPost]
    public IActionResult Crear(Producto producto)
    {
        prodRep.Create(producto);
        return RedirectToAction("Listar");
    } 

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}