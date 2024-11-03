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

    [HttpGet]
    public IActionResult Modificar(int idProd)
    {
        var producto = prodRep.GetProducto(idProd);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Modificar(Producto productoView)
    {
        var producto = prodRep.GetProducto(productoView.IdProducto);

        producto.Descripcion = productoView.Descripcion;
        producto.Precio = productoView.Precio;

        prodRep.Update(producto);

        return RedirectToAction("Listar");
    }

    [HttpGet]
    public IActionResult Eliminar(int idProd)
    {
        var producto = prodRep.GetProducto(idProd);
        return View(producto);
    }

    [HttpPost]
    public IActionResult EliminarConfirm(int idProd)
    {    
        prodRep.delete(idProd);
        return RedirectToAction("Listar");
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}